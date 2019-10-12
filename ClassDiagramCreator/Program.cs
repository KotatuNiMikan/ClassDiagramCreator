using System.Linq;

namespace ClassDiagramCreator
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            string assemblyFile = args[0];
            string targetTypeName = "Trial.WindowsFormsApp.TextEditor.MainForm";
            var classDiagramCleator = ClassDiagramCleator.CreateInstance(assemblyFile, targetTypeName);
            classDiagramCleator.OutputFile($"_{targetTypeName.Split('.').Last()}.puml");
        }
    }
}
