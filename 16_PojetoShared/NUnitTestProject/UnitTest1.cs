using MSTest_NUnit;

namespace NUnitTestProject
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        [TestCase(18)]
        [TestCase(60)]
        [TestCase(15)]
        public void TestarIdadeMaior18(int idade)
        {
            //int idade = 18;
            bool resultado = Helper.VerificaIdadeDeRisco(idade);
            Assert.IsTrue(resultado);
        }
    }
}