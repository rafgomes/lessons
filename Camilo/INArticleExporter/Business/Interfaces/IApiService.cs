using System.Net;
using System.Threading.Tasks;
using System.Xml.Linq;


namespace Imprensa
{
    namespace Business
    {
        public interface IApiService
        {
            Task<XDocument> LoadXDocumentAsync(string uri);

            Task<string> ReadResponse(HttpWebRequest req);

            Task PostData2GN4(string sPostURL, XElement xObjects);
        }
    }
}
