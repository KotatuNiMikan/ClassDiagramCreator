using System.Linq;

namespace ClassDiagramCreator
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            string assemblyFile = @"C:\Users\炬燵に蜜柑のノート2代目\source\repos\WindowsFormsTEST\WindowsFormsTEST\bin\Debug\WindowsFormsTEST.exe";
            string targetTypeName = "WindowsFormsTEST.Form1";
            var classDiagramCleator = ClassDiagramCleator.CreateInstance(assemblyFile, targetTypeName);
            classDiagramCleator.OutputFile($"_{targetTypeName.Split('.').Last()}.puml");
        }
    }
}
