using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INPerformanceTest.Interfaces
{
    public interface ICredentials
    {
        string Name { get; }
        string Ticket { get; }
        string Url { get; }
    }
}
