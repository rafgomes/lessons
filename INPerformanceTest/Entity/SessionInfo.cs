using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INPerformanceTest.Entity
{
    public class SessionInfo
    {
        public SessionInfo(string ticket, string username, string url)
        {
            this.Ticket = ticket;
            this.Username = username;
            this.Url = url;
        }
        public string Ticket { get; private set; }
        public string Url { get; private set; }
        public string Username { get; private set; }
    }
}
