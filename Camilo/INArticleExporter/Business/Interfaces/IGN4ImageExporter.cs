using System.Threading.Tasks;

namespace Imprensa
{
    namespace Business
    {
        namespace Interfaces
        {
            public interface IGN4ImageExporter
            {
                Task ExportImg(int mediaId, string ImageName);
            }
        }
    }
}
