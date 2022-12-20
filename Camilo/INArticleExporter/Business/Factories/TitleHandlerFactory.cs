using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using INPerformanceTest.Business.Interfaces;
using INPerformanceTest.Business.Services;
using Microsoft.Extensions.Logging;

namespace Imprensa
{
    namespace Business
    {
        public class TitleHandlerFactory : ITitleHandlerFactory
        {
       
            private ICategoryService categoryService;
            private readonly ILogger logger;
            private readonly IPublicationInfo publicationInfo;

            public TitleHandlerFactory(ICategoryService categoryService, ILogger logger, IPublicationInfo publicationInfo)
            {
              
                this.categoryService = categoryService;
                this.logger = logger;
                this.publicationInfo = publicationInfo;
            }

            public async Task<ITitleHandler> GetTitHandler(List<ArticleModel> articles)
            {              
                var titArticles = (from a in articles
                                    where a.INMateriaId == "0"
                select a).ToList();

                var categories = await categoryService.GetMainCategoriesAsync(publicationInfo.TitleName);

                Parallel.ForEach(titArticles, titarticle =>
                {
                    logger.LogInformation($"Set SortBy to {titarticle.INTitDescription}");

                    titarticle.INSortBy = GetNewInSortBy(titarticle.INSortBy);

                    logger.LogInformation($"SortBy={titarticle.INSortBy}");
                });
                
                return new TitleHandler(categories, titArticles, categoryService, logger);
            }
            private string GetNewInSortBy(string insortby)
            {
                logger.LogInformation($"Set SortBy to {insortby}");

                var splittedInSortBy = insortby.Split(':');

                var newInSortBy = splittedInSortBy.FirstOrDefault();

                if ((newInSortBy == null))
                    throw new Exception($"Wrong Sort By: {insortby}");

                for (int index = 1; index <= splittedInSortBy.Count() - 1; index++)
                {
                    if (Convert.ToInt32(splittedInSortBy[index]) == 0)
                        break;

                    newInSortBy = string.Concat(newInSortBy, ":", splittedInSortBy[index]);
                }

                return newInSortBy;
            }
        }
    }
}
