using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using System.Xml;
using System.Threading;
using System.Collections.Concurrent;
using Imprensa.Business.Entities;
using Microsoft.Extensions.Logging;
using INPerformanceTest.Business.Repositories;

namespace Imprensa
{
    namespace Business
    {
        public class ArticleStatusSender : IArticleStatusSender
        {
            private List<PageArticle> allArticles;
            private readonly ILogger logger;
            private readonly IArticleRepository articleRepository;
            public ArticleStatusSender(List<PageArticle> allArticles, ILogger logger, IArticleRepository articleRepository)
            {
                this.allArticles = allArticles;
                this.logger = logger;
                this.articleRepository = articleRepository;
            }
            public async Task SendStatus(ArticleModel article, string status)
            {
                var tasks = new List<Task>();
                article.INArticleStatus = status;
                tasks.Add(articleRepository.UpdateArticleAsync(article));

                var children = this.GetArticleChidren(article);

                logger.LogInformation($"Changing status INMATERIAID={article.INMateriaId} ArticleId={article.Id}");
                logger.LogInformation($"Changing status from {children.Count} children from Article {article.INMateriaId}");

                foreach (var child in children)
                {
                    child.INArticleStatus = status;
                    logger.LogInformation($"Changing status INMATERIAID={child.INMateriaId} ArticleId={child.Id}");
                    tasks.Add(articleRepository.UpdateArticleAsync(article));
                    //await child.SaveAsync();
                }
                await Task.WhenAll(tasks);
            }
            private List<ArticleModel> GetArticleChidren(ArticleModel article)
            {
                var splittedArticles = (from doc in this.allArticles
                                        where (doc.Article.INMateriaId == article.INMateriaId & doc.Article.Id != article.Id)
                                        select new ArticleModel(doc.Article.Id)).ToList();

                var childrenMateriaObj = (from doc in this.allArticles
                                          where (doc.Article.INParentId == article.INMateriaId.ToString())
                                          select new
                                          {
                                              INMateriaId = doc.Article.INMateriaId,
                                              Id = doc.Article.Id
                                          }).ToList();
                var articles = new List<ArticleModel>();
                articles.AddRange(splittedArticles);


                foreach (var childMateriaObj in childrenMateriaObj)
                {
                    var childArticle = new ArticleModel(childMateriaObj.Id);
                    articles.Add(childArticle);
                    var childSplittedArticles = (from doc in this.allArticles
                                                 where doc.Article.INMateriaId == childMateriaObj.INMateriaId
                                                 select new ArticleModel(doc.Article.Id)).ToList();
                    articles.AddRange(childSplittedArticles);
                }
                return articles;
            }
        }
    }
}
