using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INPerformanceTest.Shared
{
    public class WebConfigHelper
    {
        public static string GetGN4URL()
        {
            var url = System.Configuration.ConfigurationManager.AppSettings["gn4url"];

            return url;
        }

        public static int GetNumberOfTry()
        {
            string numberOfTry = System.Configuration.ConfigurationManager.AppSettings["NumberOfTry"];

            return Convert.ToInt32(numberOfTry);
        }
        public static string Gn4User()
        {
            var user = System.Configuration.ConfigurationManager.AppSettings["gn4user"];

            return user;
        }
        public static string Gn4Password()
        {
            var pwd = System.Configuration.ConfigurationManager.AppSettings["gn4password"];

            return pwd;
        }
 
        public static string LogFolder()
        {
            var log = System.Configuration.ConfigurationManager.AppSettings["LogFolder"];

            return log;
        }

    }
}
