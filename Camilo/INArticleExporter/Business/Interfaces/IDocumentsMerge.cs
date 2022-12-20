using Imprensa.Business.Entities;
using System.Collections.Generic;

namespace Imprensa.Business
{
    public interface IDocumentsMerge
    {
        KafkaArticleDocument GetMergedArticle(List<KafkaArticleDocument> xDocs);
    }
}