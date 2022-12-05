using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INPerformanceTest.Shared.Log
{
    public class FileLogger : ILogger
    {
        private string filePath;
        private static object _lock = new object();
        private bool logToConsole = false;
        public FileLogger(string path)
        {
            filePath = path;
            logToConsole = false;
        }

        public FileLogger(string path, bool logToConsole)
        {
            filePath = path;
            this.logToConsole = logToConsole;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            //return logLevel == LogLevel.Trace;
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (formatter != null)
            {
                lock (_lock)
                {
                    string fullFilePath = Path.Combine(filePath, DateTime.Now.ToString("yyyy-MM-dd") + "_log.txt");
                    var n = Environment.NewLine;
                    string exc = "";
                    if (exception != null) exc = n + exception.GetType() + ": " + exception.Message + n + exception.StackTrace + n;

                    string output = logLevel.ToString() + ": " + DateTime.Now.ToString() + " " + formatter(state, exception) + n + exc;

                    File.AppendAllText(fullFilePath, output);

                    if (logToConsole)
                    {
                        Console.WriteLine(output);
                    }
                }
            }
        }
    }
}
