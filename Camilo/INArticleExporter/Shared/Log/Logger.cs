using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INPerformanceTest.Shared.Log
{

    public class GirinoLog : ILogger
    {
        IDisposable ILogger.BeginScope<TState>(TState state)
        {
            throw new NotImplementedException();
        }

        bool ILogger.IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        void ILogger.Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {

            string sql = $"insert into Log (messages) values ({formatter.ToString()}";
            //execute insert cmd(sql);

            Console.WriteLine($"Girino Log: {logLevel} =>  {state.ToString()}");
           
        }
    }


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

        public static ILogger GetGirinoLogger()
        {
            return new GirinoLog();
        }

    }
}
