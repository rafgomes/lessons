using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INPerformanceTest.Interfaces
{
    public interface ILoggerFactory
    {
        ILogger CreateLogger(string name);
    }
}
