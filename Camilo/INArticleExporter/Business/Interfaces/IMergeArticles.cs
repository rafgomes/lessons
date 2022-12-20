using Imprensa.Business.Entities;
using System.Collections.Generic;

namespace Imprensa.Business
{
    public interface IMergeArticles
    {
        List<KafkaArticleDocument> GetResults(List<KafkaArticleDocument> kafkaArticleDocs);
    }
}