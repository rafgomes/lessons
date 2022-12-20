using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using Imprensa.Business.Interfaces;
using System.Xml.Linq;
using Microsoft.Extensions.Logging;
using INPerformanceTest.Interfaces;

namespace Imprensa
{
    namespace Business
    {
        public class CategoryService : ICategoryService
        {
            private readonly ICategoryRepository categoryRepository;
            private readonly IApiService apiService;
            private readonly ILogger logger;
            private readonly ICredentials credentials;

            public CategoryService(ICategoryRepository categoryRepository, IApiService apiService, ILogger logger, ICredentials credentials) {
                this.categoryRepository = categoryRepository;
                this.apiService = apiService;
                this.logger = logger;
                this.credentials = credentials;
            }

            public Task<string> GetCategoryPathAsync(int INPortalCategoryId)
            {
                return categoryRepository.GetCategoryPathAsync(INPortalCategoryId);
            }        

            public Task<XElement> GetCategoryTreeAsync(string incategoryId, string inSortBy = "")
            {
                return categoryRepository.GetCategoryTreeAsync(incategoryId, inSortBy);
            }

            public Task<string> GetEdaviCategoryAsync(string insortby)
            {
                return categoryRepository.GetEdaviCategoryAsync(insortby);
            }

            public async Task<List<CategoryModel>> GetMainCategoriesAsync(string titlename)
            {
                var strDoUrl = string.Format("{0}/do.ashx?ticket={1}&cmd=search&name=gn4:categorySet[@name='{2}']&feed={3}", credentials.Url, credentials.Ticket, "IMPNACCategories", "GSIAutoPaginationMainCategories");

                if ((titlename.Contains("DODF")))
                    strDoUrl = string.Format("{0}/do.ashx?ticket={1}&cmd=search&name=gn4:category[@name='DODF']&feed={2}", credentials.Url, credentials.Ticket, "GSIAutoPaginationMainCategories");

                var categories = new List<CategoryModel>();

                XDocument xDoc = await apiService.LoadXDocumentAsync(strDoUrl);
                if (xDoc!=null)
                {
                    XNamespace gn4 = "urn:schemas-teradp-com:gn4tera";
                    var xCategories = xDoc.Descendants("category");

                    foreach (XElement xCategory in xCategories)
                    {
                        var category = new CategoryModel(Convert.ToInt32(xCategory.Attribute("id").Value));
                        category.Description = xCategory.Attribute("desc").Value;

                        category.Name = xCategory.Attribute("name").Value;
                        category.INSortBy = xCategory.Attribute("sortby").Value;
                        category.Code = xCategory.Attribute("code").Value;
                        category.IdSiorg = xCategory.Attribute("idsiorg").Value;
                        category.Path = xCategory.Attribute("path").Value;

                        logger.LogInformation($"Adding Main Category {category.Name} with INSORTBY {category.INSortBy}");

                        categories.Add(category);
                    }
                }

                return categories;
            }
        }
    }
}
