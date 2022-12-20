using Imprensa.Business;
using System.Threading.Tasks;

namespace INPerformanceTest.Business.Repositories
{
    public interface IArticleRepository
    {
        Task UpdateArticleAsync(ArticleModel article);
    }
}