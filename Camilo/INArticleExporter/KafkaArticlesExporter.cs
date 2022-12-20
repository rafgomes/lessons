using Imprensa.Business;
using INPerformanceTest.Business.Interfaces;
using INPerformanceTest.Business.Loaders;
using INPerformanceTest.Shared.Log;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INPerformanceTest
{
    public class KafkaArticlesExporter : IKafkaArticlesExporter
    {
        private readonly IPageArticlesLoader pageArticlesLoader;
        private readonly IPublishArticlesLoader publishArticlesLoader;
        private readonly IPublicationInfo publicationInfo;
        private readonly IKafkaArticleDocumentLoader kafkaArticleDocumentLoader;
        private readonly IImageExporter imageExporter;
        private readonly IKafkaDocumentExtrator kafkaDocumentExtrator;
        private readonly IKafkaPublisher kafkaPublisher;
        private readonly ILogger logger;

        public KafkaArticlesExporter(IPageArticlesLoader pageArticlesLoader, IPublishArticlesLoader publishArticlesLoader, IPublicationInfo publicationInfo, IKafkaArticleDocumentLoader kafkaArticleDocumentLoader, IImageExporter imageExporter, IKafkaPublisher kafkaPublisher, ILogger logger)
        {
            this.pageArticlesLoader = pageArticlesLoader;
            this.publishArticlesLoader = publishArticlesLoader;
            this.publicationInfo = publicationInfo;
            this.kafkaArticleDocumentLoader = kafkaArticleDocumentLoader;
            this.imageExporter = imageExporter;
            this.kafkaPublisher = kafkaPublisher;
            this.logger = logger;
        }

        public async Task Export(int editionId)
        {
            try
            {
                var pageArticles = await pageArticlesLoader.GetPageArticlesFromEdition(editionId, publicationInfo.TitleName);
                var articles = (from a in pageArticles select a.Article).ToList();
                await imageExporter.Export(articles);                
                var kafkaDocs = await kafkaArticleDocumentLoader.GetKafkaArticleDocuments(pageArticles);
                var publishArticles = await publishArticlesLoader.GetPublishArticles(kafkaDocs);
                await kafkaPublisher.PublishAll(publishArticles, pageArticles);

            }catch(Exception e)
            {
                logger.LogError(e, "Error");
            }
        }
    }
}
