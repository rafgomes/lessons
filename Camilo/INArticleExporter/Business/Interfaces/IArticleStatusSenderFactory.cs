using System.Collections.Generic;
using Imprensa.Business.Entities;

namespace Imprensa
{
    namespace Business
    {
        public interface IArticleStatusSenderFactory
        {
            IArticleStatusSender CreateArticleStatusSender(List<PageArticle> allArticles);
        }
    }
}
