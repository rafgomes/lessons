using Imprensa.Business.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace INPerformanceTest.Business.Loaders
{
    public interface IKafkaArticleDocumentLoader
    {
        Task<List<KafkaArticleDocument>> GetKafkaArticleDocuments(List<PageArticle> pageArticles);
    }
}