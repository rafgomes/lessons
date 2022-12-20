using INPerformanceTest.Interfaces;
using System.Threading.Tasks;

namespace INPerformanceTest.Factories
{
    public interface ICredentialsFactory
    {
        Task<ICredentials> GetCredentialsAsync(string url, string user, string password);
    }
}