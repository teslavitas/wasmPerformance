using System.Runtime.InteropServices.JavaScript;

public class Program
{
    public static void Main(string[] args)
    {
    }
}

// inspired by https://www.meziantou.net/using-dotnet-code-from-javascript-using-webassembly.htm
// build it with dotnet publish --configuration Release

public partial class CsharpExampleClass
{
    // Make the method accessible from JS
    [JSExport]
    internal static int AddOne(int x)
    {
        return x + 1;
    }
}

public partial class Solution
{
    [JSExport]
    public static int NthMagicalNumber(int n, int a, int b)
    {
        return CSharpPayload.CSharpDirectSolution.NthMagicalNumber(n, a, b);
    }
}