using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using Microsoft.Extensions.DependencyInjection;
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

        var services = new ServiceCollection();
        services.Add
        
        foreach (var file in files)
        {
            var plantuml = GraphQLOperations.GetFileContents(file.Path)?.ReadTextFile;

            if (plantuml is null)
            {
                continue;
            }

            InputSmBuilder inputSmBuilder = new(new SmTransformer(), new DiagramToSmConverter(),
                new NameMangler(), new DrawIoToSmDiagramConverter());
            inputSmBuilder.ConvertPlantUmlTextNodesToVertices(plantUmlText);
            inputSmBuilder.FinishRunning();

            
            // TODO - do more
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