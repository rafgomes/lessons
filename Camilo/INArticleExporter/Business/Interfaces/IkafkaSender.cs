using System.Threading.Tasks;

namespace Imprensa
{
    namespace Business
    {
        namespace Interfaces
        {
            public interface IKafkaSender
            {
                Task SendMetadatos(string json, string topic, string key);
                Task SendArticle(string json, string topic, string key);
            }
        }
    }
}
