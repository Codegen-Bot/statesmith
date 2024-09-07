using System;
using System.IO;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using Microsoft.Extensions.DependencyInjection;
using StateSmith;
using StateSmith.Common;
using StateSmith.Input;
using StateSmith.Input.Antlr4;
using StateSmith.Input.DrawIo;
using StateSmith.Input.PlantUML;
using StateSmith.Output.Algos.Balanced1;
using StateSmith.Runner;
using StateSmith.SmGraph;

namespace StateSmithBot;

public class StateSmithMiniBot : IMiniBot
{
    public void Execute()
    {
        var configuration = GraphQLOperations.GetConfiguration().Configuration;
        var files = GraphQLOperations.GetFiles(configuration.PlantUmlFiles, []).Files ?? [];

        CustomFile.ReadTextFile = path => GraphQLOperations.GetFileContents(path).ReadTextFile;
        CustomFile.WriteTextFile = (path, contents) => GraphQLOperations.AddFile(path, contents);

        var language = configuration.Language switch
        {
            StateSmithLanguage.C99 => TranspilerId.C99,
            StateSmithLanguage.Java => TranspilerId.Java,
            StateSmithLanguage.CSharp => TranspilerId.CSharp,
            StateSmithLanguage.JavaScript => TranspilerId.JavaScript,
            _ => TranspilerId.CSharp,
        };
        
        foreach (var file in files)
        {
            // var plantuml = GraphQLOperations.GetFileContents(file.Path)?.ReadTextFile;
            //
            // if (plantuml is null)
            // {
            //     continue;
            // }

#pragma warning disable CS0219 // Variable is assigned but its value is never used
            bool diagramRan;
#pragma warning restore CS0219 // Variable is assigned but its value is never used

            // string diagramLongerPath = $"{_searchDirectory}/{diagramShortPath}";
            // string diagramAbsolutePath = Path.GetFullPath(diagramLongerPath);
            string diagramAbsolutePath = file.Path;

            // string? csxAbsPath = _runInfo.FindCsxWithDiagram(diagramAbsolutePath);
            // if (csxAbsPath != null)
            // {
            //     var csxRelativePath = Path.GetRelativePath(_searchDirectory, csxAbsPath);
            //     if (IsVerbose)
            //     {
            //         _runConsole.QuietMarkupLine($"...Skipping diagram `{diagramShortPath}` already run by csx file `{csxRelativePath}`.");
            //     }
            //     diagramRan = false;
            //     return diagramRan;
            // }

            // _runConsole.AddMildHeader($"Checking diagram: `{diagramShortPath}`");
            // _runConsole.WriteLine($"Diagram settings: {_diagramOptions.Describe()}");
            // _runConsole.QuietMarkupLine($"Change detection not implemented yet. Rebuild for diagram. Issue #328.");
            // TODO: https://github.com/StateSmith/StateSmith/issues/328
            // Need to actually check something like `_incrementalRunChecker.TestFilePath(absolutePath);`
            // IncrementalRunChecker.Result runCheck = IncrementalRunChecker.Result.NeedsRunNoInfo;
            // if (runCheck != IncrementalRunChecker.Result.OkToSkip)
            // {
            //     // already basically printed by IncrementalRunChecker
            //     //_console.WriteLine($"Script or its diagram dependencies have changed. Running script.");
            // }
            // else
            // {
            //     if (IsRebuild)
            //     {
            //         _runConsole.MarkupLine("Would normally skip (file dates look good), but [yellow]rebuild[/] option set.");
            //     }
            //     else
            //     {
            //         _runConsole.QuietMarkupLine($"Diagram and its dependencies haven't changed. Skipping.");
            //         diagramRan = false;
            //         return diagramRan; //!!!!!!!!!!! NOTE the return here.
            //     }
            // }

            //string callerFilePath = CurrentDirectory + "/";  // Slash needed for fix of https://github.com/StateSmith/StateSmith/issues/345
            string callerFilePath = "/";  // Slash needed for fix of https://github.com/StateSmith/StateSmith/issues/345
            
            RunnerSettings runnerSettings = new(diagramFile: diagramAbsolutePath, transpilerId: language);
            runnerSettings.simulation.enableGeneration = !configuration.GenerateSimulation != false; // enabled by default
            // runnerSettings.propagateExceptions = _runHandlerOptions.PropagateExceptions;
            // runnerSettings.dumpErrorsToFile = _runHandlerOptions.DumpErrorsToFile;

            // the constructor will attempt to read diagram settings from the diagram file
            SmRunner smRunner = new(settings: runnerSettings, renderConfig: null, callerFilePath: callerFilePath);

            if (smRunner.PreDiagramBasedSettingsException != null)
            {
                //_runConsole.ErrorMarkupLine("\nFailed while trying to read diagram for settings.\n");
                smRunner.PrintAndThrowIfPreDiagramSettingsException();   // need to do this before we check the transpiler ID
                throw new Exception("Should not get here.");
            }

            if (runnerSettings.transpilerId == TranspilerId.NotYetSet)
            {
                //_runConsole.MarkupLine($"Ignoring diagram as no language specified `--lang` and no transpiler ID found in diagram.");
                diagramRan = false;
                // return diagramRan; //!!!!!!!!!!! NOTE the return here.
            }

            //_runConsole.WriteLine($"Running diagram: `{diagramShortPath}`");
            smRunner.Run();
            diagramRan = true;
        }
    }
    
    private void PostProcessForKeptComments(PlantUMLWalker walker)
    {
        foreach (var keptComment in walker.keptCommentBlocks)
        {
            DiagramNode? diagramNode = KeptCommentConverter.Convert(keptComment);
            if (diagramNode != null)
            {
                walker.root.children.Add(diagramNode);
            }
        }
    }

    private bool IsNodeEntryPoint(DiagramNode node, PlantUMLWalker walker)
    {
        if (walker.nodeStereoTypeLookup.TryGetValue(node, out var stereotype))
        {
            return PlantUMLTextComparer.IsEntryPointStereotype(stereotype);
        }

        return false;
    }

    private bool IsNodeExitPoint(DiagramNode node, PlantUMLWalker walker)
    {
        if (walker.nodeStereoTypeLookup.TryGetValue(node, out var stereotype))
        {
            return PlantUMLTextComparer.IsExitPointStereotype(stereotype);
        }

        return false;
    }

    /// <summary>
    /// See https://github.com/StateSmith/StateSmith/issues/3
    /// </summary>
    /// <param name="walker"></param>
    private void PostProcessForEntryExit(PlantUMLWalker walker)
    {
        foreach (var edge in walker.edges)
        {
            // transitions coming into an entry point need to be adjusted
            if (IsNodeEntryPoint(edge.target, walker))
            {
                edge.label += "via entry " + edge.target.label;
                edge.target = edge.target.parent.ThrowIfNull();
            }

            // transitions from an exit point need to be adjusted
            if (IsNodeExitPoint(edge.source, walker))
            {
                edge.label += "via exit " + edge.source.label;
                edge.source = edge.source.parent.ThrowIfNull();
            }
        }

        foreach (var item in walker.nodeStereoTypeLookup)
        {
            var node = item.Key;
            var stereotype = item.Value;

            if (PlantUMLTextComparer.IsEntryPointStereotype(stereotype))
            {
                node.label = "entry : " + node.label;
            }
            else if (PlantUMLTextComparer.IsExitPointStereotype(stereotype))
            {
                node.label = "exit : " + node.label;
            }
        }
    }

    /// See https://github.com/StateSmith/StateSmith/issues/40
    /// <param name="walker"></param>
    private void PostProcessForChoicePoint(PlantUMLWalker walker)
    {
        foreach (var item in walker.nodeStereoTypeLookup)
        {
            var node = item.Key;
            var stereotype = item.Value;

            if (PlantUMLTextComparer.IsChoicePointStereotype(stereotype))
            {
                node.label = "$choice : " + node.label;
            }
        }
    }

    private PlantUMLParser BuildParserForString(string inputString, ErrorListener errorListener)
    {
        // Slight hack to work around ANTL4 limitation
        // https://github.com/StateSmith/StateSmith/issues/352
        const char START_OF_FILE = '\u0001';
        inputString = START_OF_FILE + inputString;

        ICharStream stream = CharStreams.fromString(inputString);
        var lexer = new PlantUMLLexer(stream);
        lexer.RemoveErrorListeners(); // prevent antlr4 error output to console
        lexer.AddErrorListener(errorListener);

        ITokenStream tokens = new CommonTokenStream(lexer);
        PlantUMLParser parser = new(tokens)
        {
            BuildParseTree = true
        };
        parser.RemoveErrorListeners(); // prevent antlr4 error output to console
        parser.AddErrorListener(errorListener);

        return parser;
    }
}