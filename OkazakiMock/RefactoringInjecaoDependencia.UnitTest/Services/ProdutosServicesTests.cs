namespace RefactoringInjecaoDependencia.UnitTest.Services
{
    using RefactoringInjecaoDeDependencia.Services;
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using RefactoringInjecaoDeDependencia.Dtos;
    using RefactoringInjecaoDeDependencia.Repositories;
    using Microsoft.Extensions.Logging;

    [TestClass]
    public class ProdutosServicesTests
    {
        private ProdutosServices _testClass;
        private Mock<IProdutoRepository> _produtoRepository;
        private Mock<ILogger> _logger;

        [TestInitialize]
        public void SetUp()
        {
            _produtoRepository = new Mock<IProdutoRepository>();
            _logger = new Mock<ILogger>();
            _testClass = new ProdutosServices(_produtoRepository.Object, _logger.Object);
        }

        [TestMethod]
        public void CanConstruct()
        {
            // Act
            var instance = new ProdutosServices(_produtoRepository.Object, _logger.Object);

            // Assert
            Assert.IsNotNull(instance);
        }

        [TestMethod]
        public void CannotConstructWithNullProdutoRepository()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new ProdutosServices(default(IProdutoRepository), new Mock<ILogger>().Object));
        }

        [TestMethod]
        public void CannotConstructWithNullLogger()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new ProdutosServices(new Mock<IProdutoRepository>().Object, default(ILogger)));
        }

        [TestMethod]
        public void CanCallGetProduto()
        {


            // Arrange
            var produtoID = 435280595;
            var produtoCodigo = 127462721;

            _produtoRepository.Setup(mock => mock.Obter(It.IsAny<int>())).Returns(new ProdutoDto(produtoCodigo, "TestValue1078390674", 1101914263.89M, true));

            // Act
            var result = _testClass.GetProduto(produtoID);

            // Assert
            _produtoRepository.Verify(mock => mock.Obter(It.IsAny<int>()), Times.Once);

            _logger.Verify(
                x => x.Log(
                    LogLevel.Information,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((o, t) => string.Equals($"Produto: {produtoID}", o.ToString(), StringComparison.InvariantCultureIgnoreCase)),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
                Times.Once);



            Assert.AreEqual(produtoCodigo, result.Codigo);
            //Assert.Fail("Create or modify test");
        }

        [TestMethod]
        public void SholdLogErrorWhenObterThrowsAnException()
        {


            // Arrange
            var produtoID = 435280595;
            var produtoCodigo = 127462721;

            _produtoRepository.Setup(mock => mock.Obter(It.IsAny<int>())).Throws(new Exception("Deu Merda"));
            //_produtoRepository.Setup(mock => mock.Obter(It.IsAny<int>())).Returns(new ProdutoDto(produtoCodigo, "TestValue1078390674", 1101914263.89M, true));

            // Act
            //var result = _testClass.GetProduto(produtoID);
            Assert.ThrowsException<Exception>(() => _testClass.GetProduto(produtoID));

            // Assert
            _produtoRepository.Verify(mock => mock.Obter(It.IsAny<int>()), Times.Once);

            _logger.Verify(
                x => x.Log(
                    LogLevel.Information,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((o, t) => string.Equals($"Produto: {produtoID}", o.ToString(), StringComparison.InvariantCultureIgnoreCase)),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
                Times.Once);

            _logger.Verify(
                x => x.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((o, t) => string.Equals("Deu Merda", o.ToString(), StringComparison.InvariantCultureIgnoreCase)),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
                Times.Once);



            //Assert.AreEqual(produtoCodigo, result.Codigo);
            //Assert.Fail("Create or modify test");
        }
    }
}