using System.Collections.Generic;
using Imprensa.Business.Entities;

namespace Imprensa
{
    namespace Business
    {
        public interface IPrepareJsonMetadatosFactory
        {
            IPrepareJsonMetadatos CreatePrepareJsonMetadatos(List<PublishArticle> publishArticles);
        }
    }
}
