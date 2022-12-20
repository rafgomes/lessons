using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Imprensa.Business.Entities;


public interface IWebDocumentManager
{
    Task<List<KafkaArticleDocument>> GetKafkaArticles();
}
