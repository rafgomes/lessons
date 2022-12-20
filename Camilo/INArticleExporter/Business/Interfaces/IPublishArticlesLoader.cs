using Imprensa.Business.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IPublishArticlesLoader
{
    Task<List<PublishArticle>> GetPublishArticles(List<KafkaArticleDocument> kafkadocs);
}