using Imprensa.Business.Entities;

namespace Imprensa.Business
{
    public interface IMergeRelated
    {
        KafkaArticleDocument GetMergedArticle();
    }
}