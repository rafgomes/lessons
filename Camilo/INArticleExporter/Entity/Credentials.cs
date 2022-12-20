using INPerformanceTest.Commons;
using INPerformanceTest.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INPerformanceTest.Entity
{
  
    class Credentials : ICredentials
    {
        public Credentials(string name, string ticket, string url)
        {
            Name = name;
            Ticket = ticket;
            Url = url;
        }
        public string Name { get; }

        public string Ticket { get; }

        public string Url { get; }
    }
}
