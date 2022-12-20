using Imprensa.Business;
using INPerformanceTest.Business.Interfaces;
using INPerformanceTest.Extensions;
using INPerformanceTest.Interfaces;
using System.IO;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
namespace INPerformanceTest.Config.Factory
{
    public class ConfigFactory : IConfigFactory
    {
        private readonly IApiService apiService;
        private readonly ICredentials credentials;
        public ConfigFactory(IApiService apiService, ICredentials credentials)
        {
            ExpressionExtensions.CheckForNullArgument(() => apiService, () => credentials);

            this.apiService = apiService;
            this.credentials = credentials;         
        }

        public async Task<IConfig> GetConfig(string configName = "")
        {
            if (string.IsNullOrEmpty(configName))
            {
                configName = "IMPNACCONFIG";
            }

            string url = $"{credentials.Url}/do.ashx?cmd=config&name=IMPNACCONFIG&ticket={credentials.Ticket}";
            XDocument doc = await apiService.LoadXDocumentAsync(url);
            XmlDocument xmlDocument = doc.ToXmlDocument();
         
            var RootOutputFolder = GetStringByPath(xmlDocument, "//IMPNACConfig/PortalDestFolder");
          
            var config = new IMPNAConfig()
            {
                PortalDestFolder = RootOutputFolder,
                KafkaDOArticleTopic = GetStringByPath(xmlDocument, "//IMPNACConfig/Kafka/Topic/Do/Publication"),
                KafkaDODFArticleTopic = GetStringByPath(xmlDocument, "//IMPNACConfig/Kafka/Topic/Dodf/Publication"),
                KafkaDOMetadatosTopic = GetStringByPath(xmlDocument, "//IMPNACConfig/Kafka/Topic/Do/Metadatos"),
                KafkaDODFMetadatosTopic = GetStringByPath(xmlDocument, "//IMPNACConfig/Kafka/Topic/Dodf/Metadatos"),
                BrokerList = GetStringByPath(xmlDocument, "//IMPNACConfig/Kafka/Config/Server"),
                ClientId = GetStringByPath(xmlDocument, "//IMPNACConfig/Kafka/Config/ClientId"),
                MessageTimeoutMs = GetStringByPath(xmlDocument, "//IMPNACConfig/Kafka/Config/Ssl/MessageTimeoutMs"),
                KeyLocation = GetStringByPath(xmlDocument, "//IMPNACConfig/Kafka/Config/Ssl/KeyLocation"),
                CertificateLocation = GetStringByPath(xmlDocument, "//IMPNACConfig/Kafka/Config/Ssl/CertificateLocation"),
                CaLocation = GetStringByPath(xmlDocument, "//IMPNACConfig/Kafka/Config/Ssl/CaLocation"),
                KeyPassword = GetStringByPath(xmlDocument, "//IMPNACConfig/Kafka/Config/Ssl/KeyPassword"),
                MessageSizeBytes = GetStringByPath(xmlDocument, "//IMPNACConfig/Kafka/Config/Message/Size"),
                ConnectionString = GetStringByPath(xmlDocument, "//IMPNACConfig/INDatabase/GnConectionString")
            };

            return config;
        }
        private string GetStringByPath(XmlDocument xdoc, string path)
        {
            if (xdoc == null)
                return string.Empty;

            XmlNode node = xdoc.SelectSingleNode(path);
            if (node == null)
                return string.Empty;

            return node.InnerXml;
        }
    }
}
