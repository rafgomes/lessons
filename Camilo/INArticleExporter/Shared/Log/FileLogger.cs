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
        private string fileName = string.Empty;
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
        public FileLogger(string path, string fileName,  bool logToConsole)
        {
            filePath = path;
            this.logToConsole = logToConsole;
            this.fileName = fileName;
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

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> callback)
        {
            if (callback != null)
            {
                lock (_lock)
                {
                    string fullFilePath = Path.Combine(filePath, DateTime.Now.ToString("yyyy-MM-dd") + "_log.txt");

                    if (!String.IsNullOrEmpty(fileName))
                         fullFilePath = Path.Combine(filePath, $"{DateTime.Now.ToString("yyyy-MM-dd")}_{fileName}.txt");

                    var n = Environment.NewLine;
                    string exc = "";

                    Console.WriteLine(callback(state, exception));


                    if (exception != null) exc = n + exception.GetType() + ": " + exception.Message + n + exception.StackTrace + n;

                    string output = logLevel.ToString() + ": " + DateTime.Now.ToString() + " " + callback(state, exception) + n + exc;

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
