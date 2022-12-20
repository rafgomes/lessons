using Imprensa.Business.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace INPerformanceTest.Business.Loaders
{
    public interface IPageArticlesLoader
    {
        Task<List<PageArticle>> GetPageArticlesFromEdition(int editionId, string titleName);
    }
}