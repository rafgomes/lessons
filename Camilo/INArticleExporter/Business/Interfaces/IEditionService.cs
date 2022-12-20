using INPerformanceTest.Business.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INPerformanceTest.Business.Interfaces
{
    public interface IEditionService
    {
        Task<List<EditionModel>> GetGroupEditionsById(int editionId);
        Task<IPublicationInfo> GetPublicationInfoByEditionId(int editionId);
    }
}
