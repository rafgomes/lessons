using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INPerformanceTest.Shared.Log
{
    public class Logger
    {
        public static ILogger GetLogger(string folder, bool logToConsole = false)
        {
            var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder
                    .AddFilter("Microsoft", LogLevel.Warning)
                    .AddFilter("System", LogLevel.Warning)
                    .AddFilter("NonHostConsoleApp.Program", LogLevel.Debug);
            });

            loggerFactory.AddFile(folder, logToConsole);
            ILogger logger = loggerFactory.CreateLogger<Program>(); 

            return logger; 
        }

    }
}
