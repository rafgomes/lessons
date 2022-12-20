using System.Threading.Tasks;

namespace Imprensa.Business
{
    public interface IArticleStatusSender
    {
        Task SendStatus(ArticleModel article, string status);
    }
}