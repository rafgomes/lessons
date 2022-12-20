using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Imprensa.Business
{
    public interface IKafkaService
    {
        Task<List<XDocument>> LoadKafkaDocsFromEditionAsync(int editionId);
        Task<XDocument> LoadKafkaAsync(string id, string title);
        Task<XDocument> LoadWebAsync(string id, string Title);
        Task SaveStatus(ArticleModel article);
    }
}