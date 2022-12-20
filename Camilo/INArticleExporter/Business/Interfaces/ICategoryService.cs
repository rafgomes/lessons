using INPerformanceTest.Attributes;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Imprensa.Business
{


    public interface ICategoryService
    {
        [Cache]
        Task<string> GetCategoryPathAsync(int INPortalCategoryId);
        [Cache]
        Task<XElement> GetCategoryTreeAsync(string incategoryId, string inSortBy = "");
        [Cache]
        Task<string> GetEdaviCategoryAsync(string insortby);
        [Cache]
        Task<List<CategoryModel>> GetMainCategoriesAsync(string titlename);

    }
}