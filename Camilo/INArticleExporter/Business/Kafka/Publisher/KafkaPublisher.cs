using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using Imprensa.Business.Interfaces;
using Imprensa.Business.Entities;
using Microsoft.Extensions.Logging;
using INPerformanceTest.Business.Helpers;
using INPerformanceTest.Business.Interfaces;
using INPerformanceTest.Business.Entities.Models;

namespace Imprensa
{
    namespace Business
    {
        public class KafkaPublisher : IKafkaPublisher
        {
            protected IKafkaSender sender;
            private readonly IPublishArticlesLoader prepareJsonArticles;
            private readonly IArticleStatusSenderFactory articleStatusSenderFactory;
            private readonly IPrepareJsonMetadatosFactory prepareJsonMetadatosFactory;
            private readonly ILogger debugLogger;
            private readonly ILogger logger;
            protected IConfig config;
            private readonly IPublicationInfo publicationInfo;
            
            public KafkaPublisher(IKafkaSender kafkaSender, IPublishArticlesLoader prepareJsonArticles, IArticleStatusSenderFactory articleStatusSenderFactory, IPrepareJsonMetadatosFactory prepareJsonMetadatosFactory, INPerformanceTest.Interfaces.ILoggerFactory loggerFactory, ILogger logger, IConfig config, IPublicationInfo publicationInfo)
            {
                this.sender = kafkaSender;
                this.prepareJsonArticles = prepareJsonArticles;
                this.articleStatusSenderFactory = articleStatusSenderFactory;
                this.prepareJsonMetadatosFactory = prepareJsonMetadatosFactory;
                this.debugLogger = loggerFactory.CreateLogger("Debug");
                this.logger = logger;
                this.config = config;
                this.publicationInfo = publicationInfo;
            }
            public async Task PublishAll(List<PublishArticle> publishArticles, List<PageArticle> allArticles)
            {
                try
                {              

                    await PublishArticles( allArticles, publishArticles);
                    await PublishMetadatos( publishArticles);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Issues while publishing to Kafka");
                    throw ex;
                }
            }

            public virtual async Task PublishArticles(List<PageArticle> allArticles, List<PublishArticle> publishArticles)
            {
                Exception exception = null;

                try
                {
                    var jsonArticles = publishArticles;

                    var topic = this.config.KafkaDOArticleTopic;

                    var stopWatch = new Stopwatch();

                    if ((publicationInfo.TitleName.Contains("DODF")))
                        topic = this.config.KafkaDODFArticleTopic;

                    stopWatch.Start();

                    debugLogger.LogInformation($"Publicando {jsonArticles.Count()} jsons");

                    debugLogger.LogInformation($"Publicando pro Kakfa...");

                    var pushblishArticle = jsonArticles[0];

                    await SendArticle(topic, pushblishArticle, allArticles);

                    logger.LogInformation("Esperado 45 segundos para enviar primeiro json ao Kafka");

                    Thread.Sleep(45000);

                    var chunkJsonArticles = Utils.BuildChunksWithRange(jsonArticles, 20);

                    foreach (var chunkedJsonArticles in chunkJsonArticles)
                    {

                        // Dim listTasks = New List(Of Task)
                        foreach (var item in chunkedJsonArticles)
                        {
                            Task task = SendArticle(topic, item, allArticles);
                            // listTasks.Add(task)
                            await task;
                        }
                    }

                    stopWatch.Stop();
                    debugLogger.LogInformation($"TEMPO total após início publicação ao Kafka: {(stopWatch.ElapsedMilliseconds) / (double)1000} segundos");
                }
                catch (Exception ex)
                {
                    throw exception;
                }
            }
            public async Task PublishMetadatos(List<PublishArticle> publishArticles)
            {
                var topic = this.config.KafkaDOMetadatosTopic;

                if ((publicationInfo.TitleName.Contains("DODF")))
                    topic = this.config.KafkaDODFMetadatosTopic;

                var timestamp = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");

                var dataPubEfetiva = publicationInfo.PublicationDate.ToString("yyyy-MM-dd");
                var idJornal = publicationInfo.WebEditionNumber;
                var key = string.Format("{0}+{1}+{2}", idJornal, dataPubEfetiva, timestamp);

                var prepareMetadatos = this.prepareJsonMetadatosFactory.CreatePrepareJsonMetadatos(publishArticles);
                var json = prepareMetadatos.GetJson();

                logger.LogInformation("Metadados: {0}", json);

                await sender.SendMetadatos(json, topic, key);
            }

            private async Task SendArticle(string topic, PublishArticle publishArticle, List<PageArticle> allArticles)
            {
                try
                {
                    logger.LogInformation($"Sending to Kafka {publishArticle.Article.INMateriaId} Key: {publishArticle.Key}");
                    await sender.SendArticle(publishArticle.Json, topic, publishArticle.Key);
                    logger.LogInformation($"Updating Article Status {publishArticle.Article.INMateriaId}");
                    await SendStatus(publishArticle, allArticles);
                }
                catch (Exception ex)
                {
                    debugLogger.LogError(ex, $"Error tryging to publish json {publishArticle.Json} at {topic} with key: {publishArticle.Key}");

                    throw ex;
                }
            }
            private async Task SendStatus(PublishArticle PublishArticle, List<PageArticle> allArticles)
            {
                var articleStatusSender = articleStatusSenderFactory.CreateArticleStatusSender(allArticles);
                await articleStatusSender.SendStatus(PublishArticle.Article, "Published");
            }
         
            [Obsolete("Method is deprecated because we need to change status after publish to kafka.")]
            private async Task PublishStatus(List<PublishArticle> publishArticles, List<PageArticle> allArticles)
            {
                var publishedArticles = (from p in publishArticles
                                         where p.isPublished == true
                                         select p).ToList();
                logger.LogInformation($"Updating status {publishedArticles.Count} articles");

                var chunkJsonArticles = Utils.BuildChunksWithRange(publishedArticles, 20);

                var articleStatusSender = articleStatusSenderFactory.CreateArticleStatusSender(allArticles);

                foreach (var chunkedJsonArticles in chunkJsonArticles)
                {
                    var tasks = new List<Task>();
                    foreach (var item in chunkedJsonArticles)
                    {
                        logger.LogInformation($"Update Status Article INMATERIA={item.Article.INMateriaId} - ArticleId={item.Article.Id}");

                        await articleStatusSender.SendStatus(item.Article, "Published");
                    }
                }
            }
        }
    }
}
