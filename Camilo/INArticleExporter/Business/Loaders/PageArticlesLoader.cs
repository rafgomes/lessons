using Imprensa.Business;
using Imprensa.Business.Entities;
using Imprensa.Business.Interfaces;
using INPerformanceTest.Business.Entities.Models;
using INPerformanceTest.Business.Interfaces;
using INPerformanceTest.Business.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INPerformanceTest.Business.Loaders
{
    public class PageArticlesLoader : IPageArticlesLoader
    {
        private readonly IEditionService editionService;
        private readonly IPageArticleApiService pageArticleApiService;

        public PageArticlesLoader(IEditionService editionService, IPageArticleApiService pageArticleApiService)
        {
            this.editionService = editionService;
            this.pageArticleApiService = pageArticleApiService;
        }

        public async Task<List<PageArticle>> GetPageArticlesFromEdition(int editionId, string titleName)
        {
            List<EditionModel> editions = await editionService.GetGroupEditionsById(editionId);

            List<PageArticle> pageArticles = await pageArticleApiService.GetPageArticles(editions, titleName);

            if(pageArticles.Count == 0)
            {
                throw new Exception($"There is no articles connected to Edition {editionId}");
            }

            return pageArticles;
        }
    }
}
