using INPerformanceTest.Business.Entities.Models;
using INPerformanceTest.Business.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace INPerformanceTest.Business.Services
{
    public interface IEditionRepository
    {
        Task<IPublicationInfo> GetPublicationInfoByEditionId(int editionId);
        Task<List<EditionModel>> GetGroupEditionsById(int editionId);
    }
}