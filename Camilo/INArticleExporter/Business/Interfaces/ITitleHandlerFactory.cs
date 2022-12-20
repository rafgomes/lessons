using System.Collections.Generic;
using System.Threading.Tasks;

namespace Imprensa.Business
{
    public interface ITitleHandlerFactory
    {
        Task<ITitleHandler> GetTitHandler(List<ArticleModel> articles);
    }
}