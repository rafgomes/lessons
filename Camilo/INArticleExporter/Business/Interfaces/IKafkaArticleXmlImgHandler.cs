using System.Xml.Linq;

namespace Imprensa.Business
{
    public interface IKafkaArticleXmlImgHandler
    {
        void AddBase64Img(XDocument xdoc);
    }
}