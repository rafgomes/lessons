using RefactoringInjecaoDeDependencia.Dtos;

namespace RefactoringInjecaoDeDependencia.Services
{
    public interface IProdutosServices
    {
        ProdutoDto GetProduto(int produtoID);
    }
}