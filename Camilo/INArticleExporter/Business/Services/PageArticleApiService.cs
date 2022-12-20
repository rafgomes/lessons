
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using INPerformanceTest.Business.Entities.Models;
using Imprensa.Business.Entities;
using Imprensa.Business.Interfaces;
using INPerformanceTest.Business.Interfaces;
using Microsoft.Extensions.Logging;
using ILoggerFactory = INPerformanceTest.Interfaces.ILoggerFactory;
using INPerformanceTest.Business.Services;
using System.Xml.Linq;
using Confluent.Kafka;

namespace Imprensa
{
    namespace Business
    {
        public class PageArticleApiService : IPageArticleApiService
        {
            private readonly IEditionService editionService;
            private readonly IArticleService articleServices;
            private readonly IPageService pageService;
            private readonly ILogger logger;
            private readonly ILogger performanceLogger;
            private List<PageArticle> pageArticles;
     
            public PageArticleApiService(IArticleService articleServices, IPageService pageService, ILogger logger, ILoggerFactory loggerFactory)
            {
                this.articleServices = articleServices;
                this.pageService = pageService;
                this.logger = logger;
                this.performanceLogger = loggerFactory.CreateLogger("Performance");
                pageArticles = new List<PageArticle>();
            }
            public async Task<List<PageArticle>> GetPageArticles(List<EditionModel> editions, string titleName)
            {
                logger.LogInformation("Loading Articles");

                var listTask = new List<Task>();

                foreach (EditionModel edModel in editions)
                {
                    await ProcessPageArticlesFromEdition(edModel, titleName);                    
                }

                // Await Task.WhenAll(listTask.ToArray())

                logger.LogInformation($"PageArticleList Count={pageArticles.Count}");

                return pageArticles;
            }
            private async Task ProcessPageArticlesFromEdition(EditionModel editionModel, string titleName)
            {
                var editionProperties = new EditionProperties(titleName, editionModel.PublicationDate);
                performanceLogger.LogInformation("LoadAllPagesAsync");
                List<PageModel> pages = await pageService.GetPagesByEditionAsync(editionModel.Id);

                if (pages == null || pages.Count == 0)
                    return;


                var processedArticles = new List<int>();
                var savedDocument = new Queue<XDocument>();

                var listTask = new List<Task>();
                var orderingPages = pages.OrderBy(a => a.Number);
                performanceLogger.LogInformation("ProcessPageArticle");
            
                foreach (PageModel page in orderingPages)
                {
                    await ProcessArticlesPerPage(editionModel,  page);
                   
                }
            }
            private async Task ProcessArticlesPerPage(EditionModel edModel, PageModel page)
            {

                List<ArticleModel> articles = await articleServices.GetArticlesByPageAsync(page.Id, false);

                if(articles == null || articles.Count == 0) return;

                List<PageArticle> currentPageArticles = (from a in articles
                                                  select new PageArticle()
                                                  {
                                                      Article = a,
                                                      PageNum = page.Number,                                                     
                                                      EditionModel = edModel
                                                  }).OrderBy(a => a.PageNum).ToList();
                logger.LogInformation($"Page Article Count={pageArticles.Count}");


                foreach (var pageArticle in currentPageArticles)
                {
                    if ((!pageArticles.Exists(k => k.Article.Id == pageArticle.Article.Id)))
                    {
                        pageArticles.Add(pageArticle);

                        logger.LogInformation($"Article INMateriaid={pageArticle.Article.INMateriaId} PageNumber={pageArticle.PageNum}");
                    }
                    else
                    {
                        var article = pageArticles.Where(a => a.Article.Id == pageArticle.Article.Id).SingleOrDefault();

                        if ((article.PageNum > pageArticle.PageNum))
                            article.PageNum = pageArticle.PageNum;

                        logger.LogInformation($"Article Already Exists: INMateriaid={pageArticle.Article.INMateriaId} PageNumber={pageArticle.PageNum}");
                    }
                }
            }
        }
    }
}
