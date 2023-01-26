namespace LearningMocks
{
    using LibTesting;
    using System;
    using NUnit.Framework;
    using NSubstitute;
    using NSubstitute.ExceptionExtensions;
    using System.Security.Cryptography.X509Certificates;

    [TestFixture]
    public class PagamentosManagerTests
    {
        private PagamentosManager _testClass;
        private IPagamentoService _pagamentoService;
        private IEmailService _emailService;
        private ILoga _logger;

        [SetUp]
        public void SetUp()
        {
            _pagamentoService = Substitute.For<IPagamentoService>();
            _emailService = Substitute.For<IEmailService>();
            _logger = Substitute.For<ILoga>();
            _testClass = new PagamentosManager(_pagamentoService, _emailService, _logger);
        }

        [Test]
        public void CanConstruct()
        {
            // Act
            var instance = new PagamentosManager(_pagamentoService, _emailService, _logger);

            // Assert
            Assert.That(instance, Is.Not.Null);
        }

        [Test]
        public void CannotConstructWithNullPagamentoService()
        {
            Assert.Throws<ArgumentNullException>(() => new PagamentosManager(default(IPagamentoService), Substitute.For<IEmailService>(), Substitute.For<ILoga>()));
        }

        [Test]
        public void CannotConstructWithNullEmailService()
        {
            Assert.Throws<ArgumentNullException>(() => new PagamentosManager(Substitute.For<IPagamentoService>(), default(IEmailService), Substitute.For<ILoga>()));
        }

        [Test]
        public void CannotConstructWithNullLogger()
        {
            Assert.Throws<ArgumentNullException>(() => new PagamentosManager(Substitute.For<IPagamentoService>(), Substitute.For<IEmailService>(), default(ILoga)));
        }

        [Test]
        public void CanCallDoPagamento()
        {
            // Arrange
            var banco = new Banco { NumeroConta = 1807063129, Nome = "TestValue1041329010", Agencia = 434263807 };
            var valor = 1394387996;
            var email = "expectedEmail@gmail.com";

            // Act
            _testClass.DoPagamento(banco, valor, email);

            // Assert
            _pagamentoService.Received().DoPagamento(banco, valor);
            _emailService.Received().SendEmail(email, $"O pagamento de {valor} foi emitido com sucesso");
            _logger.DidNotReceive().Log(Arg.Any<string>());
          
        }

        [Test]
        public void ShouldLogErrorAndSendErrorEmail_WhenDoPagamentoFails()
        {
            // Arrange
            var banco = new Banco { NumeroConta = 1807063129, Nome = "TestValue1041329010", Agencia = 434263807 };
            var valor = 1394387996;
            var email = "expectedEmail@gmail.com";
            var exception = new Exception("Deu Errro");

            _pagamentoService.When( x=> x.DoPagamento(banco, valor)).Throw(exception);
            // Act
            _testClass.DoPagamento(banco, valor, email);

            // Assert
            _pagamentoService.Received().DoPagamento(banco, valor);
            _emailService.Received().SendEmail(email, $"Houve um problema com o pagamento de {valor}");
            _logger.Received().Log(exception.Message);
        }



        [Test]
        public void CannotCallDoPagamentoWithNullBanco()
        {
            Assert.Throws<ArgumentNullException>(() => _testClass.DoPagamento(default(Banco), 1938256040, "TestValue1715692333"));
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("   ")]
        public void CannotCallDoPagamentoWithInvalidEmail(string value)
        {
            Assert.Throws<ArgumentNullException>(() => _testClass.DoPagamento(new Banco { NumeroConta = 495624653, Nome = "TestValue1860016270", Agencia = 1716149141 }, 1848035806, value));
        }
    }
}