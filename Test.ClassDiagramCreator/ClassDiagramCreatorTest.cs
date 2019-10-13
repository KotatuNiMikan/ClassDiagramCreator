using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ClassDiagramCreator
{
    [TestClass]
    public class ClassDiagramCreatorTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            var classDiagramCleator = ClassDiagramCleator.CreateInstance("ClassDiagramCreator.dll");
            classDiagramCleator.OutputFile($"ÉNÉâÉXê}.puml");
        }
    }
}
