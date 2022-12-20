using System.Threading.Tasks;

namespace Imprensa.Business
{
    public interface IJsonValidation
    {
        Task Validate(string json);
    }
}