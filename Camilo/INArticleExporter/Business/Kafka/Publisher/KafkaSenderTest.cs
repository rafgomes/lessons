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
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using System.Xml;
using Confluent.Kafka;
using System.Threading;
using System.Collections.Concurrent;
using Imprensa.Business.Interfaces;
using INPerformanceTest.Business.Interfaces;
using Microsoft.Extensions.Logging;

namespace Imprensa
{
    namespace Business
    {
        public class KafkaSenderTest : IKafkaSender
        {
            private readonly ILogger debugLogger;

            public KafkaSenderTest(INPerformanceTest.Interfaces.ILoggerFactory loggerFactory )
            {
                this.debugLogger = loggerFactory.CreateLogger("Debug");
            }

            public Task SendMetadatos(string json, string topic, string key)
            {
                debugLogger.LogInformation("Postando Metadados para Kafka:");
                debugLogger.LogInformation("Topic: " + topic);
                debugLogger.LogInformation("key: " + key);
                debugLogger.LogInformation("Json: " + json);
                return Task.Delay(0);
            }
            public Task SendArticle(string json, string topic, string key)
            {
                debugLogger.LogInformation("Postando Article para Kafka:");
                debugLogger.LogInformation("Topic: " + topic);
                debugLogger.LogInformation("key: " + key);
                debugLogger.LogInformation("Json: " + json);
                return Task.Delay(0);
            }
        }
    }
}
