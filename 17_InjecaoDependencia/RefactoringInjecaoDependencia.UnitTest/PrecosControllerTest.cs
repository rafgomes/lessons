using Castle.Components.DictionaryAdapter.Xml;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RefactoringInjecaoDeDependencia.Controllers;
using RefactoringInjecaoDeDependencia.Dtos;
using RefactoringInjecaoDeDependencia.Repositories;
using RefactoringInjecaoDeDependencia.UnitTest;

namespace RefactoringInjecaoDependencia.UnitTest
{
    [TestClass]
    [TestCategory("UnitTest > Controllers > Preco")]
    public class PrecosControllerTest
    {
        [TestMethod]
        public void DeveriaRetornarErroSeNaoEncontrarOProduto()
        {
            int idProdutoNotFound = 0;
            
            //var prodMock = new ProdutoRepositoryFake(null);
            //var taxaMock = new TaxaEntregaRepositoryFake();
            var taxaMock = new Mock<ITaxaEntregaRepository>(); //o proprio Framework vai criar uma classe que imita a interface ITaxaEntregaRepository
            taxaMock.Setup(t => t.Obter(idProdutoNotFound)).Returns(default(TaxaEntregaDto));
            var prodMock = new Mock<IProdutoRepository>();
            prodMock.Setup(p => p.Obter(idProdutoNotFound)).Returns((ProdutoDto)null);

            var precoCtrl = new PrecosController(prodMock.Object, taxaMock.Object);


            var res = precoCtrl.ObterPreco(idProdutoNotFound, 111111);
            
            Assert.AreEqual(404, ((NotFoundObjectResult)res).StatusCode);
            taxaMock.Verify(t => t.Obter(idProdutoNotFound), Times.Never);
            prodMock.Verify(p => p.Obter(idProdutoNotFound), Times.Once);
        }

        [TestMethod]
        public void DeveriaChamarTaxaEntregaSeProdutoRetornadoTiverTaxaDeEntrega()
        {
            int produto1 = 1;
            int cepEntrega = 999555;


            ProdutoDto radio = new ProdutoDto(produto1, "LG", 12.12M, true);

            TaxaEntregaDto tx = new TaxaEntregaDto(1, cepEntrega, 52.52M);

            //var prodMock = new ProdutoRepositoryFake(null);
            //var taxaMock = new TaxaEntregaRepositoryFake();
            var taxaMock = new Mock<ITaxaEntregaRepository>(); //o proprio Framework vai criar uma classe que imita a interface ITaxaEntregaRepository
            taxaMock.Setup(t => t.Obter(cepEntrega)).Returns(tx);
            var prodMock = new Mock<IProdutoRepository>();
            prodMock.Setup(p => p.Obter(produto1)).Returns(radio);

            var precoCtrl = new PrecosController(prodMock.Object, taxaMock.Object);


            var res = precoCtrl.ObterPreco(produto1, cepEntrega);

            //Assert.AreEqual(404, ((NotFoundObjectResult)res).StatusCode);
            var objRes = (ObterPrecoResponse)((OkObjectResult)res).Value;

            Assert.AreEqual(636.54m, objRes.ValorEntrega);
            Assert.AreEqual(648.66m, objRes.PrecoTotal);
            taxaMock.Verify(t => t.Obter(cepEntrega), Times.Once);
            prodMock.Verify(p => p.Obter(produto1), Times.Once);
        }

        [TestMethod]
        public void NaoDeveriaChamarTaxaEntregaSeProdutoRetornadoTiverTaxaDeEntregaFalse()
        {
            int produto1 = 1;
            int cepEntrega = 999555;


            ProdutoDto radio = new ProdutoDto(produto1, "LG", 12.12M, false);

            TaxaEntregaDto tx = new TaxaEntregaDto(1, cepEntrega, 52.52M);

            //var prodMock = new ProdutoRepositoryFake(null);
            //var taxaMock = new TaxaEntregaRepositoryFake();
            var taxaMock = new Mock<ITaxaEntregaRepository>(); //o proprio Framework vai criar uma classe que imita a interface ITaxaEntregaRepository
            taxaMock.Setup(t => t.Obter(cepEntrega)).Returns(tx);
            var prodMock = new Mock<IProdutoRepository>();
            prodMock.Setup(p => p.Obter(produto1)).Returns(radio);

            var precoCtrl = new PrecosController(prodMock.Object, taxaMock.Object);


            var res = precoCtrl.ObterPreco(produto1, cepEntrega);

            //Assert.AreEqual(404, ((NotFoundObjectResult)res).StatusCode);
            var objRes = (ObterPrecoResponse)((OkObjectResult)res).Value;

            Assert.AreEqual(radio.Preco, objRes.ValorProduto);
            Assert.AreEqual(0, objRes.ValorEntrega);
            Assert.AreEqual(radio.Preco, objRes.PrecoTotal);
            taxaMock.Verify(t => t.Obter(cepEntrega), Times.Never);
            prodMock.Verify(p => p.Obter(produto1), Times.Once);
        }

        [TestMethod]
        public void DeveriaRetornarPrecoTotalIgualAValorProduto()
        {
            //var prodMock = new ProdutoRepositoryFake(new ProdutoDto(33, "Novo Produto", 1022.33m, false));
            var prodMock = new Mock<IProdutoRepository>();
            prodMock.Setup(m => m.Obter(It.IsAny<int>())).Returns(new ProdutoDto(10, "Novo Produto10", 1011.50m, false));
            prodMock.Setup(m => m.Obter(1)).Returns(new ProdutoDto(1, "Novo Produto1", 1011.50m, false));
            prodMock.Setup(m => m.Obter(2)).Returns(new ProdutoDto(2, "Novo Produto2", 1011.50m, false));
            

            var taxaMock = new TaxaEntregaRepositoryFake();
            var precoCtrl = new PrecosController(prodMock.Object, taxaMock);

            var res = precoCtrl.ObterPreco(20, 75021010);
            var objRes = (ObterPrecoResponse)((OkObjectResult)res).Value;
            
            Assert.AreEqual(0, objRes.ValorEntrega);
            Assert.AreEqual(1022.33m, objRes.PrecoTotal);
            Assert.AreEqual(1022.33m, objRes.ValorProduto);
        }

        [TestMethod]
        public void DeveriaRetornarValorEntregaEPrecoTotalIgualASomaDoProdutoEEntrega()
        {
            var prodMock = new ProdutoRepositoryFake(new ProdutoDto(1, "TV 75 polegadas", 7044.75m, true));
            var taxaMock = new TaxaEntregaRepositoryFake();
            var precoCtrl = new PrecosController(prodMock, taxaMock);

            var res = precoCtrl.ObterPreco(1, 75021010);
            var objRes = (ObterPrecoResponse)((OkObjectResult)res).Value;
            
            Assert.AreEqual(211.34m, objRes.ValorEntrega);
            Assert.AreEqual(7044.75m, objRes.ValorProduto);
            Assert.AreEqual(7256.09m, objRes.PrecoTotal);
        }
    }
}
