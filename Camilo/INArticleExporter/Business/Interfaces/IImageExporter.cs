using System.Collections.Generic;
using System.Threading.Tasks;

namespace Imprensa.Business
{
    public interface IImageExporter
    {
        Task Export(ArticleModel article);
        Task Export(List<ArticleModel> articleModels);
    }
}