using Imprensa.Business;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace INPerformanceTest.Business.Services
{
    public interface IArticleService
    {
        Task<List<ArticleModel>> GetArticlesByPageAsync(int pageId, bool isWebXml);
    }
}