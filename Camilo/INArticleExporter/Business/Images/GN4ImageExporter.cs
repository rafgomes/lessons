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

using Imprensa.Business.Interfaces;
using Microsoft.Extensions.Logging;
using INPerformanceTest.Interfaces;
using System.Net;
using ICredentials = INPerformanceTest.Interfaces.ICredentials;
using INPerformanceTest.Business.Interfaces;
using INPerformanceTest.Business.Services;

namespace Imprensa
{
    namespace Business
    {

        public class GN4ImageExporterTest : IGN4ImageExporter
        {
            public async Task ExportImg(int mediaId, string ImageName)
            {
                await Task.Delay(0);
                Console.WriteLine($"ExportImg: {mediaId} - {ImageName}");
            }
        }

        public class GN4ImageExporter : IGN4ImageExporter
        {
            private IConfig config;
            private readonly IPublicationInfo publicationInfo;
            private readonly ILogger logger;
            private readonly ICredentials credentials;

            public GN4ImageExporter(IConfig config, IPublicationInfo publicationInfo, ILogger logger, ICredentials credentials)
            {
                this.config = config;
                this.publicationInfo = publicationInfo;
                this.logger = logger;
                this.credentials = credentials;
            }
            public async Task ExportImg(int mediaId, string ImageName)
            {
                var destFolder = Path.Combine(config.PortalDestFolder, publicationInfo.PublicationDate.ToString("yyyyMMdd"), publicationInfo.TitleName);

                var fileImage = System.IO.Path.Combine(destFolder, ImageName + ".jpg");
                var pars = string.Format("MediaId:{0};Path:{1}", mediaId, System.IO.Path.Combine(destFolder, ImageName + ".jpg"));
                string strDoUrl = string.Format("{0}/do.ashx?ticket={1}&cmd=wf&Name={2}&Pars={3}", credentials.Url, credentials.Ticket, "GSIExportImages", pars);
                logger.LogInformation("Exporting IMAGE: " + strDoUrl);
                WebRequest request = WebRequest.Create(strDoUrl);
                request.Timeout = 300000;
                WebResponse response = await request.GetResponseAsync();
            }
        }
    }
}
