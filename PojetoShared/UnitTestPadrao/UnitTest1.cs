using MSTest_NUnit;

namespace UnitTestPadrao
{
    [TestClass]
    public class UnitTest1
    {
        [TestInitialize]
        public void Inicializar()
        {

        }
        
        [TestMethod]
        [DataRow(18)]
        [DataRow(15)]
        [DataRow(60)]
        public void TestarIdadeMaior18(int idade)
        {
            //int idade = 18;
            bool resultado = Helper.VerificaIdadeDeRisco(idade);
            Assert.IsTrue(resultado);
        }

        //[TestMethod]
        //public void TestarNomeRisco()
        //{
        //    string nome = "Rafael";
        //    bool resultado = Helper.VerificaNomeDeRisco(nome);
        //    Assert.IsTrue(resultado);
        //}
    }
}