using System.Threading.Tasks;
using System.Xml.Linq;

namespace Imprensa.Business
{
    public interface ITitleHandler
    {
        Task<XElement> GetOrigemXmlElement(ArticleModel article);
    }
}