using Imprensa.Business;
using Imprensa.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INPerformanceTest.Business.Loaders
{
    public class KafkaArticleDocumentLoader : IKafkaArticleDocumentLoader
    {
        private readonly IPageArticlesLoader pageArticlesLoader;
        private readonly IKafkaDocumentExtrator kafkaDocumentExtrator;
        private readonly IMergeArticles mergeArticles;

        public KafkaArticleDocumentLoader(IKafkaDocumentExtrator kafkaDocumentExtrator, IMergeArticles mergeArticles)
        {
            this.kafkaDocumentExtrator = kafkaDocumentExtrator;
            this.mergeArticles = mergeArticles;
        }
        public async Task<List<KafkaArticleDocument>> GetKafkaArticleDocuments(List<PageArticle> pageArticles)
        {
            var kafkaArticleDocs = await kafkaDocumentExtrator.GetKafkaDocuments(pageArticles);

            if (kafkaArticleDocs.Count == 0)
                throw new Exception("Edição não tem matérias para serem publicadas");
       
            var result = mergeArticles.GetResults(kafkaArticleDocs);

            return result;
        }
    }
}
