using INPerformanceTest.Business.Interfaces;
using System.Threading.Tasks;

namespace INPerformanceTest.Factories
{
    public interface IPublicationInfoFactory
    {
        Task<IPublicationInfo> GetPublicationInfo(int ediionId);
    }
}