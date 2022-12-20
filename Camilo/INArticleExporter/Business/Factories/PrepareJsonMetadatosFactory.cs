using System.Collections.Generic;
using Imprensa.Business.Entities;
using INPerformanceTest.Business.Interfaces;
using Microsoft.Extensions.Logging;

namespace Imprensa
{
    namespace Business
    {
        public class PrepareJsonMetadatosFactory : IPrepareJsonMetadatosFactory
        {
            private readonly ILogger logger;
            private readonly IPublicationInfo publicationInfo;

            public PrepareJsonMetadatosFactory(ILogger logger, IPublicationInfo publicationInfo)
            {
                this.logger = logger;
                this.publicationInfo = publicationInfo;
            }
            public IPrepareJsonMetadatos CreatePrepareJsonMetadatos(List<PublishArticle> publishArticles)
            {
                return new PrepareJsonMetadatos(publishArticles, logger, publicationInfo);
            }
        }
    }
}
