using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using Imprensa.Business.Entities;
using INPerformanceTest.Business.Entities.Json;
using INPerformanceTest.Business.Interfaces;
using Microsoft.Extensions.Logging;

namespace Imprensa
{
    namespace Business
    {
        public class PrepareJsonMetadatos : IPrepareJsonMetadatos
        {
            private List<PublishArticle> publishArticles;
            private readonly ILogger logger;
            private readonly IPublicationInfo publicationInfo;

            public PrepareJsonMetadatos(List<PublishArticle> publishArticles, ILogger logger, IPublicationInfo publicationInfo)
            {
                this.publishArticles = publishArticles;
                this.logger = logger;
                this.publicationInfo = publicationInfo;
            }
            public string GetJson()
            {
                var jsonJornal = new JsonMetadatosBuilder(publicationInfo).GetJsonJornal(this.publishArticles);

                var stream = new MemoryStream();
                var contract = new DataContractJsonSerializer(typeof(JsonJornal));

                contract.WriteObject(stream, jsonJornal);

                stream.Position = 0;
                var SR = new StreamReader(stream);

                var jsonString = SR.ReadToEnd();

                logger.LogInformation("Metadados[1]: {0}", jsonString);

                var convertJsonType = new JsonConvertTypes(jsonString);
                var newstringJson = convertJsonType.GetJson();

                newstringJson = jsonString.Replace(@"\/", "/");
                logger.LogInformation("Metadados[2]: {0}", newstringJson);

                return newstringJson;
            }
        }
    }
}
