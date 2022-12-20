using System.Threading.Tasks;

namespace INPerformanceTest
{
    public interface IKafkaArticlesExporter
    {
        Task Export(int editionId);
    }
}