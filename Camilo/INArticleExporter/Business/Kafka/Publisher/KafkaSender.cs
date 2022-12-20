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
using System.Threading;
using System.Collections.Concurrent;
using Imprensa.Business.Interfaces;
using Confluent.Kafka;
using Microsoft.Extensions.Logging;
using INPerformanceTest.Business.Interfaces;

namespace Imprensa
{
    namespace Business
    {
        public class KafkaSender : IKafkaSender
        {
            private ProducerConfig producerConfig;
            private readonly ILogger logger;
            private readonly ILogger debugLogger;
            private readonly IConfig config;

            public KafkaSender(ILogger logger, INPerformanceTest.Interfaces.ILoggerFactory loggerFactory, IConfig config)
            {                
                this.logger = logger;
                this.debugLogger = loggerFactory.CreateLogger("Debug");
                this.config = config;
                producerConfig = GetProducerConfig();
            }
            public async Task SendMetadatos(string json, string topic, string key)
            {
                try
                {
                    logger.LogInformation("Postando para Kafka:");
                    logger.LogInformation("Topic: " + topic);
                    logger.LogInformation("key: " + key);
                    logger.LogInformation("Json: " + json);

                    var message = new Message<string, string>();
                    message.Key = key;
                    message.Value = json;

                    var producer = new ProducerBuilder<string, string>(producerConfig);
                    using (var produce = producer.Build())
                    {
                        var dr = await produce.ProduceAsync(topic, message);
                        logger.LogInformation(string.Format("delivered to: {0}", dr.TopicPartitionOffset));
                    }
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Some issue with Kafka");
                    throw new Exception("Houve algum problmea com o envio para o Kafka");
                }
            }

            public async Task SendArticle(string json, string topic, string key)
            {
                try
                {
                    debugLogger.LogInformation("Postando para Kafka:");
                    debugLogger.LogInformation("Topic: " + topic);
                    debugLogger.LogInformation("key: " + key);
                    debugLogger.LogInformation("MessageMaxSize: " + producerConfig.MessageMaxBytes);
                    debugLogger.LogInformation(json);

                    var message = new Message<string, string>();
                    message.Key = key;
                    message.Value = json;

                    var producer = new ProducerBuilder<string, string>(producerConfig);
                    using (var produce = producer.Build())
                    {
                        var dr = await produce.ProduceAsync(topic, message);
                        logger.LogInformation(string.Format("delivered to: {0}", dr.TopicPartitionOffset));
                    }
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Some issue with Kafka");
                    throw new Exception("Ocorreu algum erro na publicação para o Kafka");
                }
            }

            private ProducerConfig GetProducerConfig()
            {
                int messageSizeBytes = 0;
                if( ! Int32.TryParse(config.MessageSizeBytes, out messageSizeBytes))
                {
                    messageSizeBytes = 80000000;
                }
             
                var Config = new ProducerConfig()
                {
                    ClientId = config.ClientId,
                    BootstrapServers = config.BrokerList,
                    ApiVersionRequestTimeoutMs = 300000,
                    SecurityProtocol = SecurityProtocol.Ssl,
                    MessageTimeoutMs = 3600000,
                    RequestTimeoutMs = 900000,
                    MetadataRequestTimeoutMs = 900000,
                    SocketTimeoutMs = 300000,
                    SslKeyLocation = config.KeyLocation,
                    SslCertificateLocation = config.CertificateLocation,
                    SslCaLocation = config.CaLocation,
                    SslKeyPassword = config.KeyPassword,
                    StatisticsIntervalMs = 0,
                    MessageMaxBytes = messageSizeBytes
                };
                return Config;
            }
        }
    }
}
