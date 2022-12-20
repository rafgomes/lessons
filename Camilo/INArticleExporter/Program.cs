using INPerformanceTest.Entity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using INPerformanceTest.Shared.Log;
using INPerformanceTest.Commons;
using System.Security.Policy;
using System.Xml.Linq;
using System.Diagnostics;
using INPerformanceTest.Shared;
using INPerformanceTest.Interfaces;
using Unity;
using Imprensa.Business;
using INPerformanceTest.Business.Loaders;
using INPerformanceTest.Config.Factory;
using INPerformanceTest.Business.Interfaces;
using INPerformanceTest.Business.Services;
using INPerformanceTest.Business.Repositories;
using INPerformanceTest.Factories;
using Imprensa.Business.Interfaces;
using LoggerFactory = INPerformanceTest.Factories.LoggerFactory;
using ILoggerFactory = INPerformanceTest.Interfaces.ILoggerFactory;
using static System.Net.Mime.MediaTypeNames;

namespace INPerformanceTest
{
    internal class Program
    {  
        static async Task Main(string[] args)
        {
            if (args.Length == 0)
            {
                return;
            }
            bool testOnly = false;
            if(args.Length == 2)
            {
                testOnly = Convert.ToBoolean(args[1]);
            }

            int editionId = Convert.ToInt32(args[0]);
            UnityContainer container = new UnityContainer();
            ContainerConfigManager configuration = new ContainerConfigManager(container);
            configuration.Configure(editionId, testOnly: testOnly);

            IKafkaArticlesExporter articleExporter = container.Resolve<IKafkaArticlesExporter>();

            await articleExporter.Export(editionId);

            Console.WriteLine("Complated!");
        }

    }
}

