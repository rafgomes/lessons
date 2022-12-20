using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INPerformanceTest.Shared.Log
{
    public class FileLoggerProvider : ILoggerProvider
    {
        private string path;
        private bool logToConsole;
        public FileLoggerProvider(string _path)
        {
            path = _path;
            logToConsole = false;
        }

        public FileLoggerProvider(string _path, bool logToConsole)
        {
            path = _path;
            this.logToConsole = logToConsole;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new FileLogger(path, logToConsole);
        }

        public void Dispose()
        {
        }
    }
}
