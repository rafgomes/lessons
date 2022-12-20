using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Imprensa.Business.Entities;
using INPerformanceTest.Business.Entities.Models;

namespace Imprensa
{
    namespace Business
    {
        namespace Interfaces
        {
            public interface IPageArticleApiService
            {
                Task<List<PageArticle>> GetPageArticles(List<EditionModel> editions, string titleName);
              
            }
        }
    }
}
