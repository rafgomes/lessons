using System.Collections.Generic;
using Imprensa.Business.Entities;
using Microsoft.Extensions.Logging;
using INPerformanceTest.Business.Repositories;

namespace Imprensa
{
    namespace Business
    {
        public class ArticleStatusSenderFactory : IArticleStatusSenderFactory
        {
            private readonly ILogger logger;
            private readonly IArticleRepository articleRepository;

            public ArticleStatusSenderFactory(ILogger logger, IArticleRepository articleRepository) {
                this.logger = logger;
                this.articleRepository = articleRepository;
            }

            public IArticleStatusSender CreateArticleStatusSender(List<PageArticle> allArticles )
            {
                return new ArticleStatusSender(allArticles, logger, articleRepository);
            }
        }
    }
}
