using Imprensa.Business.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Imprensa.Business
{
    public interface IKafkaDocumentExtrator
    {
        Task<List<KafkaArticleDocument>> GetKafkaDocuments(List<PageArticle> pageArticles);
    }
}