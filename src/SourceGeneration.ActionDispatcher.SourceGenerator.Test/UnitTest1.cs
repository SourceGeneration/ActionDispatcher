namespace SourceGeneration.ActionDispatcher.SourceGenerator.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            string source = @"

using System;
using SourceGeneration.ActionDispatcher;

public struct Handler
{  
    [ActionHandler]
    public static void Handle(DateTime datetime) { }
}
";
            var result = CSharpTestGenerator.Generate<ActionRoutesSourceGenerator>(source, typeof(IActionDispatcher).Assembly);
            var script = result.RunResult.GeneratedTrees.FirstOrDefault()?.GetText();

        }
    }
}