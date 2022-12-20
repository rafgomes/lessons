using Imprensa.Business;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace INPerformanceTest.Business.Services
{
    public class ApiService : IApiService
    {
        private int _maxAttempts = 10;
        public async Task<XDocument> LoadXDocumentAsync(string uri)
        {
            bool success = false;
            int attempts = 1;

            XDocument result = null;

            while (!success && attempts <= _maxAttempts)
            {
                try
                {
                    Task<XDocument> t = new Task<XDocument>(() => XDocument.Load(uri));
                    t.Start();
                    result= await  t;
                    success = true;                    
                }
                catch
                {
                    if (attempts >= _maxAttempts)
                    {
                        throw;
                    }
                }
                attempts++;
            }
          
            return result;
        }
        public async Task<string> ReadResponse(HttpWebRequest req)
        {
            using (WebResponse resp = await req.GetResponseAsync())
            {
                using (System.IO.StreamReader sr = new System.IO.StreamReader(resp.GetResponseStream()))
                {
                    string respBody = await sr.ReadToEndAsync();
                    return respBody;
                }
            }
        }
        public async Task PostData2GN4(string sPostURL, XElement xObjects)
        {
            try
            {
                var builder = new StringBuilder();
                var settings = new System.Xml.XmlWriterSettings();
                settings.Indent = false;

                using (var writer = System.Xml.XmlWriter.Create(builder, settings))
                {
                    xObjects.WriteTo(writer);
                }
                var sPostXml = builder.ToString();
                var bytes = System.Text.Encoding.UTF8.GetBytes(sPostXml);
                var request = (HttpWebRequest)System.Net.WebRequest.Create(sPostURL);
                request.ContentType = "text/xml; encoding='utf-8'";
                request.ContentLength = bytes.Length;
                request.Method = "POST";
                request.Timeout = 20000;
                var requestStream = request.GetRequestStream();
                requestStream.Write(bytes, 0, bytes.Length);
                requestStream.Close();

                var bodyResponse = await ReadResponse(request);
            }
            catch (WebException wex)
            {
                if (wex != null)
                {
                    var errorResponse = wex.Response;
                    var reader = new StreamReader(errorResponse.GetResponseStream());
                    var errorMessage = reader.ReadToEnd();
                    if (!string.IsNullOrEmpty(errorMessage))
                    {
                        var xHtml = XElement.Parse(errorMessage);
                        var xGenericException = xHtml.Descendants("GenericException").FirstOrDefault();
                        if (xGenericException != null & xGenericException.Element("Msg") != null)
                        {
                            var msgErrors = xGenericException.Element("Msg").Value;
                            throw new Exception(msgErrors);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
              
    }
}
