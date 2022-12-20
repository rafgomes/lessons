using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INPerformanceTest.Shared.Log
{
    public static class FileLoggerExtensions
    {
        public static ILoggerFactory AddFile(this ILoggerFactory factory, string filePath)
        {
            factory.AddProvider(new FileLoggerProvider(filePath));
            return factory;
        }

        public static ILoggerFactory AddFile(this ILoggerFactory factory, string filePath, bool logToConsole)
        {
            factory.AddProvider(new FileLoggerProvider(filePath, logToConsole));
            return factory;
        }

    }
}
