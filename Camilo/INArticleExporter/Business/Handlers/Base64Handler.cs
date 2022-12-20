using System;
using INPerformanceTest.Business.Interfaces;

using INPerformanceTest.Business.Helpers;
using INPerformanceTest.Business.Services;
using System.IO;
using INPerformanceTest.Interfaces;
using Microsoft.Extensions.Logging;
using ILoggerFactory = INPerformanceTest.Interfaces.ILoggerFactory;

namespace Imprensa
{
    namespace Business
    {
        public class Base64HandlerTest : IBase64Handler
        {
            public string ConvertToBase64(string name)
            {
                Console.WriteLine($"Image name converted to base64: {name}");
                return $"testing{name}";
            }
        }

        public class Base64Handler : IBase64Handler
        {
            private string portalDestFolder;
            private readonly IConfig config;
            private readonly IPublicationInfo publicationInfo;
            private readonly ILogger debugLogger;

            public Base64Handler(IConfig config, IPublicationInfo publicationInfo, ILoggerFactory loggerFactory)
            {
                this.portalDestFolder = config.PortalDestFolder;
                this.config = config;
                this.publicationInfo = publicationInfo;
                this.debugLogger = loggerFactory.CreateLogger("Debug");
            }

            public Base64Handler(string destFolder)
            {
                this.portalDestFolder = destFolder;
            }

            public string ConvertToBase64(string name)
            {
                if ((!name.Contains(".jpg")))
                    name = name + ".jpg";

                var destFolder = Path.Combine(portalDestFolder, publicationInfo.PublicationDate.ToString("yyyyMMdd"), publicationInfo.TitleName);

                var imagePath = System.IO.Path.Combine(destFolder, name);

                debugLogger.LogInformation($"Converting image to Base64: {imagePath}");

                var base64ImageRepresentation = Utils.ImageToBase64(imagePath);

                if ((string.IsNullOrEmpty(base64ImageRepresentation.Trim())))
                    throw new Exception("Image is Empty");

                var validator = new Base64ImgValidator();
                validator.validate(base64ImageRepresentation);

                return base64ImageRepresentation;
            }
        }
    }
}
