using Imprensa.Business.Entities;
using INPerformanceTest.Business.Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Imprensa.Business
{
    public interface IKafkaPublisher
    {
        Task PublishAll(List<PublishArticle> publishArticles, List<PageArticle> allArticles);
        Task PublishArticles(List<PageArticle> allArticles, List<PublishArticle> publishArticles);
        Task PublishMetadatos(List<PublishArticle> publishArticles);
    }
}