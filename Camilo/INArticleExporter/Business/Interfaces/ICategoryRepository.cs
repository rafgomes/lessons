using INPerformanceTest.Attributes;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Imprensa.Business
{
    public interface ICategoryRepository 
    {
       
        Task<string> GetCategoryPathAsync(int INPortalCategoryId);
  
        Task<XElement> GetCategoryTreeAsync(string incategoryId, string inSortBy = "");
    
        Task<string> GetEdaviCategoryAsync(string insortby);     
       

    }
}