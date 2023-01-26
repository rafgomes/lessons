using Microsoft.Extensions.Logging;
using RefactoringInjecaoDeDependencia.Dtos;
using RefactoringInjecaoDeDependencia.Repositories;
using System;

namespace RefactoringInjecaoDeDependencia.Services
{
    public class ProdutosServices : IProdutosServices
    {
        private readonly IProdutoRepository produtoRepository;
        private readonly ILogger logger;

        public ProdutosServices(IProdutoRepository produtoRepository, ILogger logger)
        {
            if (produtoRepository == null || logger == null)
            {
                throw new ArgumentNullException("Não aceita parametros nulos!");
            }
            this.produtoRepository = produtoRepository;
            this.logger = logger;
        }

        public ProdutoDto GetProduto(int produtoID)
        {
            try
            {
                logger.LogInformation($"Produto: {produtoID}");
                return produtoRepository.Obter(produtoID);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                throw ex;
            }


        }
    }
}
