using System.Threading.Tasks;

namespace INPerformanceTest.Config.Factory
{
    public interface IConfigFactory
    {
        Task<IConfig> GetConfig(string configName = "");
    }
}