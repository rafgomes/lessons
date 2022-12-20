using Imprensa.Business;
using INPerformanceTest.Business.Services;
using INPerformanceTest.Commons;
using INPerformanceTest.Entity;
using INPerformanceTest.Interfaces;
using INPerformanceTest.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace INPerformanceTest.Factories
{
    public class CredentialsFactory : ICredentialsFactory
    {
        private readonly IApiService apiService;

        public CredentialsFactory(IApiService apiService)
        {
            this.apiService = apiService;
        }

        public async Task<ICredentials> GetCredentialsAsync(string url, string user, string password)
        {
            string appname = "GN3ToGN4";  
            //We create login session
            string strDoUrl = $"{url}/do.ashx?cmd=login&name={user}&pwd={password}&AppName={appname}";
         
            XDocument xdoc = await apiService.LoadXDocumentAsync(strDoUrl);

            if (xdoc != null)
            {
                XNamespace res = "http://www.teradp.com/schemas/GN4/1/Results.xsd";
                string ticket = xdoc.Element(res + "result").Element(res + "loginResult").Attribute("ticket").Value;
                Credentials credentials = new Credentials(user, ticket, url);
                return credentials;
            }
            
             throw new Exception("Could not login");            
        }
    }
}
