using INPerformanceTest.Interfaces;
using INPerformanceTest.Shared.Log;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ILoggerFactory = INPerformanceTest.Interfaces.ILoggerFactory;

namespace INPerformanceTest.Factories
{
    public class LoggerFactory : ILoggerFactory
    {       
        public ILogger CreateLogger(string name)
        {
            var logger = new FileLogger(@"C:\tera\gn4\bin\logs", name, false);
            return logger;
        }
    }
}
