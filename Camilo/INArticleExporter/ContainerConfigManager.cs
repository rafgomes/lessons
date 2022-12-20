using Imprensa.Business;
using Imprensa.Business.Interfaces;
using INPerformanceTest.Business.Interfaces;
using INPerformanceTest.Business.Loaders;
using INPerformanceTest.Business.Repositories;
using INPerformanceTest.Business.Services;
using INPerformanceTest.Config.Factory;
using INPerformanceTest.Factories;
using INPerformanceTest.Interfaces;
using INPerformanceTest.Shared;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using ILoggerFactory = INPerformanceTest.Interfaces.ILoggerFactory;
using LoggerFactory = INPerformanceTest.Factories.LoggerFactory;

namespace INPerformanceTest
{
    public class ContainerConfigManager
    {
        private readonly IUnityContainer container;

        public ContainerConfigManager(IUnityContainer container)
        {
            this.container = container;
        }

        public void Configure(int editionId, bool testOnly = false)
        {
            string url = WebConfigHelper.GetGN4URL();
            string username = WebConfigHelper.Gn4User();
            string password = WebConfigHelper.Gn4Password();

            container.RegisterType<IApiService, ApiService>();
            container.RegisterType<ICredentialsFactory, CredentialsFactory>();
            container.RegisterInstance<ICredentials>(container.Resolve<ICredentialsFactory>().GetCredentialsAsync(url, username, password).Result);
            container.RegisterType<IConfigFactory, ConfigFactory>();
            IConfig config = container.Resolve<IConfigFactory>().GetConfig("IMPNACconfig").Result;
            container.RegisterInstance<IConfig>(config);
            container.RegisterType<ILoggerFactory, LoggerFactory>();
            container.RegisterInstance<ILogger>(container.Resolve<ILoggerFactory>().CreateLogger("INLog"));
            container.RegisterType<IEditionRepository, EditionRepository>();
            container.RegisterType<IEditionService, EditionService>();
            container.RegisterInstance<IPublicationInfo>(container.Resolve<IEditionService>().GetPublicationInfoByEditionId(editionId).Result);
            container.RegisterType<IArticleRepository, ArticleRepository>();
            container.RegisterType<IArticleService, ArticleService>();
            container.RegisterType<ICategoryRepository, CategoryRepository>();
            container.RegisterType<ICategoryService, CategoryService>();
            container.RegisterType<IKafkaService, KafkaService>();
            container.RegisterType<IPageService, PageService>();
            container.RegisterType<IPageArticleApiService, PageArticleApiService>();
            container.RegisterType<IDocumentMergeFactory, DocumentMergeFactory>();
            container.RegisterType<IMergeArticles, MergeArticles>();
            container.RegisterType<IKafkaArticleDocumentLoader, KafkaArticleDocumentLoader>();
            container.RegisterType<IPageArticlesLoader, PageArticlesLoader>();
            container.RegisterType<IArticleStatusSenderFactory, ArticleStatusSenderFactory>();
            container.RegisterType<IPrepareJsonMetadatosFactory, PrepareJsonMetadatosFactory>();
            container.RegisterType<ITitleHandlerFactory, TitleHandlerFactory>();
            if (testOnly)
            {
                container.RegisterType<IBase64Handler, Base64HandlerTest>();
            }
            else{
                container.RegisterType<IBase64Handler, Base64Handler>();
            }            
            container.RegisterType<IKafkaArticleXmlImgHandler, KafkaArticleXmlImgHandler>();
            container.RegisterType<IJsonValidation, JsonValidation>();
            container.RegisterType<IJsonChangesHandler, JsonChangesHandler>();
            container.RegisterType<ITitleHandlerFactory, TitleHandlerFactory>();
            container.RegisterType<IPublishArticlesLoader, PublishArticlesLoader>();
            container.RegisterType<IPrepareJsonMetadatos, PrepareJsonMetadatos>();
            container.RegisterType<IKafkaDocumentExtrator, KafkaDocumentExtrator>();
            container.RegisterType<IArticleStatusSenderFactory, ArticleStatusSenderFactory>();
            container.RegisterType<IGN4ImageExporter, GN4ImageExporter>();
            if (testOnly)
            {
                container.RegisterType<IGN4ImageExporter, GN4ImageExporterTest>();
            }
            else
            {
                container.RegisterType<IGN4ImageExporter, GN4ImageExporter>();
            }

            container.RegisterType<IImageExporter, ImageExporter>();
            container.RegisterType<IKafkaPublisher, KafkaPublisher>();
            if (testOnly)
            {
                container.RegisterType<IKafkaSender,KafkaSenderTest>();
            }
            else
            {
                container.RegisterType<IKafkaSender, KafkaSender>();
            }
            container.RegisterType<IKafkaArticlesExporter, KafkaArticlesExporter>();

        }

    }
}
