using MeuPrimeiroTeste;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.IO;

namespace MyClassesTest
{
    [TestClass]
    public class FileProcessTest
    {
        private const string BAD_FILE_NAME = @"C:\Temp\Teste2.txt";
        private string _GoodFileName;

        /*Usando AAA (Arrange, Act, Assert) 
        Arrange => Inicializa as variaveis
        Act => Invocar metodos
        Assert => Efetivamente verificar a ação
         */

        //Utilizando AAA
        /*
        [TestMethod]
        public void FileNameDoesExists()
        {
            FileProcess fp = new FileProcess();
            bool fromCall;

            fromCall = fp.FileExists(@"C:\Temp\Teste.txt"); //irá verificar se o arquivo existe

            Assert.IsTrue(fromCall); //verifica se fromCall retornou true
            //Assert.Inconclusive(); //Validação para saber se realmente a função está funcionando
        }
        */

        
        public TestContext TestContext { get; set; } //Estudo de TestContext

        #region Test Intialize e Cleanup

        [TestInitialize] //o Initialize e o CleanUp não executa na camada de DLL
        public void TestInitialize()
        {
            if(TestContext.TestName == "FileNameDoesExist")
            {
                if (!string.IsNullOrEmpty(_GoodFileName))
                {
                    SetGoodFileName();
                    TestContext.WriteLine($"Creating File: {_GoodFileName}");
                    File.AppendText("Some text");
                }
            }
        }

        [TestCleanup]
        public void TestCleanUp()
        {
            if (TestContext.TestName == "FileNameDoesExist")
            {
                if (!string.IsNullOrEmpty(_GoodFileName))
                {
                    TestContext.WriteLine($"Deleting File: {_GoodFileName}");
                    File.Delete(_GoodFileName); //deleta o arquivo
                }
            }
        }

        #endregion

        [TestMethod]
        [Description("Check if a file does exist.")]
        //[Owner("Rafael")] //Defini o dono do teste, e depois pode ser agrupado
        //[Priority(0)] //prioridade da execução
        //[TestCategory("Exception")] //categoria
        
        public void FileNameDoesExists()
        {
            FileProcess fp = new FileProcess();
            bool fromCall;

            TestContext.WriteLine($"Testing File: {_GoodFileName}");
            fromCall = fp.FileExists(_GoodFileName); //irá verificar se o arquivo existe

            Assert.IsTrue(fromCall); //verifica se fromCall retornou true
            //Assert.Inconclusive(); //Validação para saber se realmente a função está funcionando
        }

        public void SetGoodFileName()
        {
            _GoodFileName = ConfigurationManager.AppSettings["GoodFileName"];
            if (_GoodFileName.Contains("[AppPath]"))
            {
                _GoodFileName = _GoodFileName.Replace("[AppPath]", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
            }
        }

        //Utilizando AAA
        [TestMethod]
        [Description("Check if a file does NOT exist.")]
        public void FileNameDoesNotExists()
        {
            FileProcess fp = new FileProcess();
            bool fromCall;

            fromCall = fp.FileExists(BAD_FILE_NAME); 

            Assert.IsFalse(fromCall); //verifica se fromCall retornou false
        }

        [TestMethod]
        [Timeout(2000)] //como é um teste de 3 segundos, ele vai dar erro, pois está limitando a 2 segundos
        public void SimulateTimeOut()
        {
            System.Threading.Thread.Sleep(3000);
        }

        /*Decorator is a structural pattern that allows adding new behaviors to objects dynamically by placing them inside special wrapper objects, 
         called decorators. Using decorators you can wrap objects countless number of times since both target objects and decorators follow the same interface. 
        The resulting object will get a stacking behavior of all wrappers. */

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void FileNameNullOrEmpty_ThrowsArgumentNullException()
        {
            FileProcess fp = new FileProcess();
            fp.FileExists("");
        }

        [TestMethod]
        
        public void FileNameNullOrEmpty_ThrowsArgumentNullException_UsingTryCatch()
        {
            FileProcess fp = new FileProcess();

            try
            {
                fp.FileExists("");
            }
            catch (ArgumentException)
            {
                //The test was a Sucess.
                return;
            }

            Assert.Fail("Fail Expected");
        }
    }

    //Função Teste>Analisar todos os testes, mostra as dlls que estão cobertas por testes, porem essa função não está disponivel no VS Community
}
