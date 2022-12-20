using INPerformanceTest.Business.Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace INPerformanceTest.Business.Services
{
    public interface IPageService
    {    
        Task<List<PageModel>> GetPagesByEditionAsync(int editionId);
    }
}