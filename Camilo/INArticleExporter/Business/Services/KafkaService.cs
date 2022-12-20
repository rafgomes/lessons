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
using System.Xml.Linq;
using Imprensa.Business.Interfaces;
using INPerformanceTest.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;


namespace Imprensa
{
    namespace Business
    {
        public class KafkaService :  IKafkaService
        {
            private readonly ILogger logger;
            private readonly ICredentials credentials;
            private readonly IApiService apiService;

            public KafkaService(ILogger logger, ICredentials credentials, IApiService apiService)
            {
                this.logger = logger;
                this.credentials = credentials;
                this.apiService = apiService;
            }

            public async Task<XDocument> LoadWebAsync(string id, string Title)
            {
                XDocument xDoc = null/* TODO Change to default(_) if this is not a reference type */;
                try
                {
                    var strDoUrl = string.Format("{0}/do.ashx?ticket={1}&cmd=feed&name=GSIExport2WebSite&ids={2}&pars=region:{3}:Print", credentials.Url, credentials.Ticket, id, Title);
                    logger.LogInformation(strDoUrl);
                    xDoc = await apiService.LoadXDocumentAsync(strDoUrl);

                    if (xDoc != null)
                        return xDoc;
                }
                catch
                {
                }

                return xDoc;
            }
            public async Task<XDocument> LoadKafkaAsync(string id, string title)
            {
                XDocument xDoc = null/* TODO Change to default(_) if this is not a reference type */;
                try
                {
                    var strDoUrl = string.Format("{0}/do.ashx?ticket={1}&cmd=feed&name=GSIExport2Kafka&ids={2}&pars=region:{3}:Print", credentials.Url, credentials.Ticket, id, title);
                    logger.LogInformation(strDoUrl);
                    xDoc = await apiService.LoadXDocumentAsync(strDoUrl);

                    if (xDoc != null)
                        return xDoc;
                }
                catch
                {
                }

                return xDoc;
            }

            public Task SaveStatus(ArticleModel article)
            {
                return Task.CompletedTask;
            }

            public Task<List<XDocument>> LoadKafkaDocsFromEditionAsync(int editionId)
            {
                throw new NotImplementedException();
            }
        }
    }
}
