using INPerformanceTest.Business.Services;
using INPerformanceTest.Entity;
using INPerformanceTest.Interfaces;
using INPerformanceTest.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace INPerformanceTest.Commons
{
    public static class Gn4Helper
    {
        private static SessionInfo _GN4Session;    
        public static async Task<Interfaces.ICredentials> GetCredentialsAsync()
        {
            var sGN4Url = WebConfigHelper.GetGN4URL();           
            string appname = "GN3ToGN4";            
            string sGN4User = WebConfigHelper.Gn4User();
            string sGN4Pwd = WebConfigHelper.Gn4Password();       

            //We create login session
            string strDoUrl = $"{sGN4Url}/do.ashx?cmd=login&name={sGN4User}&pwd={sGN4Pwd}&AppName={appname}";
            ApiService apiService = new ApiService();
            var xdoc = await apiService.LoadXDocumentAsync(strDoUrl);
            if (xdoc != null)
            {
                XNamespace res = "http://www.teradp.com/schemas/GN4/1/Results.xsd";
                string  ticket  = xdoc.Element(res + "result").Element(res + "loginResult").Attribute("ticket").Value;
                Credentials credentials = new Credentials(sGN4User, ticket, sGN4Url);
                return credentials;
            }
            else
            {
                throw new Exception("Could not login");
            }
        }
        public static SessionInfo GN4Session
        {
            get
            {
                try
                {
                    if (_GN4Session == null)
                    {

                        string paramTicket = Gn4Helper.DoGN4Login();
                        string username = Gn4Helper.DoGN4Login();
                        string url = WebConfigHelper.GetGN4URL();

                        _GN4Session = new SessionInfo(paramTicket, username, url);
                    }
                    var sGN4Url = WebConfigHelper.GetGN4URL();
                    var ticket = _GN4Session.Ticket;

                    XDocument xdoc = XDocument.Load($"{sGN4Url}/do.ashx?Cmd=LoginData&Logins={ticket}&ticket={ticket}");
                }
                catch (Exception ex)
                {

                }

                return _GN4Session;
            }
        }
        public static string DoGN4Login()
        {
            try
            {
                string appname = "GN3ToGN4";
                var sGN4Url = WebConfigHelper.GetGN4URL();
                string sGN4User = WebConfigHelper.Gn4User();
                string sGN4Pwd = WebConfigHelper.Gn4Password();
                string sLoginTicket = string.Empty;

                //We create login session
                string strDoUrl = $"{sGN4Url}/do.ashx?cmd=login&name={sGN4User}&pwd={sGN4Pwd}&AppName={appname}";
                var xdoc = XDocument.Load(strDoUrl);
                if (xdoc != null)
                {
                    XNamespace res = "http://www.teradp.com/schemas/GN4/1/Results.xsd";
                    sLoginTicket = xdoc.Element(res + "result").Element(res + "loginResult").Attribute("ticket").Value;
                }

                return sLoginTicket;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void DoGn4Logout(string sLoginTicket)
        {
            try
            {
                var sGN4Url = WebConfigHelper.GetGN4URL();
                var wr = WebRequest.Create($"{sGN4Url}/do.ashx?cmd=logout&ticket={sLoginTicket}");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public static string GetINSatus(short instatus)
        {
            switch (instatus)
            {
                case 0:
                    return "Original";
                case 1:
                    return "Edited";

                case 2:
                    return "PageReady";
       
                
                default:

                    return "Original";
            }
        }

        public static string GetINSubType(short inSubType)
        {
            switch (inSubType)
            {
                case 0:
                    return "Header";
                case 1:
                    return "Content";

                default:

                    return "Content";
            }
        }
    }
}
