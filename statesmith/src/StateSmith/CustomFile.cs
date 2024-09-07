using System;
using System.IO;

namespace StateSmith;

public static class CustomFile
{
    public static Func<string, string> ReadTextFile { get; set; }
    public static Action<string, string> WriteTextFile { get; set; }

    public static TextReader OpenText(string filePath)
    {
        var result = ReadTextFile(filePath);
        return new StringReader(result);
    }

    public static string ReadAllText(string filePath)
    {
        var result = ReadTextFile(filePath);
        return result;
    }

    public static void WriteAllText(string path, string contents)
    {
        WriteTextFile(path, contents);
    }
}
