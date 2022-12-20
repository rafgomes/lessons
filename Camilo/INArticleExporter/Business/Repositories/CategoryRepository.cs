using Imprensa.Business;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace INPerformanceTest.Business.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IConfig config;
        private readonly ILogger logger;

        public CategoryRepository(IConfig config, ILogger logger)
        {
            this.config = config;
            this.logger = logger;
        }

        public async Task<string> GetCategoryPathAsync(int INPortalCategoryId)
        {
            string gnConnectionString = config.ConnectionString;


            string sQuery = String.Format(@"SELECT  c.s_INDescription FROM s_ObjectTable o with (NOLOCK)
INNER JOIN s_CategoryTable c with (NOLOCK) on c.s_id = o.s_id
where o.s_id in (
SELECT gn_AncestorId FROM gn_DescenderTable with (NOLOCK)
INNER JOIN s_CategoryTable with (NOLOCK) on s_id = gn_DescenderId
WHERE s_code = '{0}'
) OR c.s_code = '{0}'
order by c.s_path ASC", INPortalCategoryId);


            List<String> categories = new List<string>();

            using (SqlConnection oConn = new SqlConnection(gnConnectionString))
            {
                await oConn.OpenAsync();
                SqlCommand command = new SqlCommand(sQuery, oConn);
                SqlDataReader reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    categories.Add(reader.GetString(0));
                }
            }

            if (categories.Count == 0)
                return string.Empty;

            string categoryPath = categories.Aggregate((i, j) => i + ';' + j);

            return categoryPath;

        }

        public async Task<XElement> GetCategoryTreeAsync(string incategoryId, string inSortBy = "")
        {
            var categories = new List<CategoryModel>();

            var element = new XElement("origem");

            var i = 0;

            string gnConnectionString = config.ConnectionString;

            var sQuery = string.Format(@"
                    SELECT    c.s_INDescription, c.s_code, c.s_INIdSiorg FROM s_ObjectTable o
                    INNER JOIN s_CategoryTable c on c.s_id = o.s_id
                    where o.s_id in (
                    SELECT gn_AncestorId FROM gn_DescenderTable 
                    INNER JOIN s_CategoryTable on s_id = gn_DescenderId
                    WHERE s_code like '{0}'
                    ) OR c.s_code like '{0}'
                    order by c.s_path DESC
                    ", incategoryId);

            using (var oConn = new SqlConnection(gnConnectionString))
            {
                await oConn.OpenAsync();
                var Command = new SqlCommand(sQuery, oConn);
                var reader = await Command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    var category = new CategoryModel(0);

                    category.Description = reader.GetString(0);
                    category.Code = reader.GetString(1);
                    try
                    {
                        category.IdSiorg = reader.GetInt32(2).ToString();
                    }
                    catch
                    {
                        category.IdSiorg = reader.GetString(2);
                    }

                    categories.Add(category);
                }
            }

            if ((categories.Exists(a => a.Description == "Editais e Avisos")))
            {
                logger.LogInformation("Category Editais e Avisos");
                var editaisCategory = await GetEdaviCategoryModel(inSortBy);
                if (!string.IsNullOrEmpty(editaisCategory.Code))
                    categories.Add(editaisCategory);
                categories.Reverse();
            }
            else
                logger.LogInformation("Common category");

            XElement tempElem = element;
            foreach (var category in categories)
            {
                if (i == 0)
                    element.Add(new XElement("idOrigem", category.Code),
                        new XElement("nomeOrigem", category.Description),
                        new XElement("idSiorg", category.IdSiorg),
                        new XElement("uf", null),
                        new XElement("nomeMunicipio", null),
                        new XElement("idMunicipioIbge", null));
                else
                {
                    var newElemnt = new XElement("origemPai",
                        new XElement("idOrigem", category.Code),
                        new XElement("nomeOrigem", category.Description),
                        new XElement("idSiorg", category.IdSiorg),
                        new XElement("uf", null),
                        new XElement("nomeMunicipio", null),
                        new XElement("idMunicipioIbge", null));
                    tempElem.Add(newElemnt);
                    tempElem = newElemnt;
                }

                i = i + 1;
            }
            return element;
        }

        public async Task<string> GetEdaviCategoryAsync(string insortby)
        {
            string gnConnectionString = config.ConnectionString;
            var splited = insortby.Split(':');

            var sortByNumber = Convert.ToInt32(splited[1]);

            var categorySortBy = string.Format("{0:00000000}", sortByNumber);

            var sQuery = @"select top 1 c.s_INDescription, LEN(s_path) path_length from s_CategoryTable c

                        inner join gn_DescenderTable d on c.s_id = d.gn_DescenderId
                        WHERE
                        c.s_INSortBy = @insortby
                         AND
                        d.gn_AncestorId in (select  o.s_id from s_ObjectTable o  where o.s_name = @categorysetname) 
                        order by path_length asc";

            var categories = new List<string>();

            using (var oConn = new SqlConnection(gnConnectionString))
            {
                await oConn.OpenAsync();
                var Command = new SqlCommand(sQuery, oConn);

                Command.CommandType = CommandType.Text;

                Command.Parameters.AddWithValue("@insortby", categorySortBy);
                Command.Parameters.AddWithValue("@categorysetname", "IMPNACCategories");
                var reader = await Command.ExecuteReaderAsync();

                while (await reader.ReadAsync())

                    categories.Add(reader.GetString(0));
            }

            if (categories.Count == 0)
                return string.Empty;

            return categories.First();
        }

           
        private async Task<CategoryModel> GetEdaviCategoryModel(string insortby)
        {
            string gnConnectionString = config.ConnectionString;
            var splited = insortby.Split(':');

            var sortByNumber = Convert.ToInt32(splited[1]);

            var categorySortBy = string.Format("{0:00000000}", sortByNumber);

            var sQuery = @"select top 1  c.s_INDescription, c.s_code, c.s_INIdSiorg , LEN(s_path) path_length from s_CategoryTable c

                        inner join gn_DescenderTable d on c.s_id = d.gn_DescenderId
                        WHERE
                        c.s_INSortBy = @insortby
                         AND
                        d.gn_AncestorId in (select  o.s_id from s_ObjectTable o  where o.s_name = @categorysetname) 
                        order by path_length asc";

            var categoryModel = new CategoryModel(0);

            using (var oConn = new SqlConnection(gnConnectionString))
            {
                await oConn.OpenAsync();
                var Command = new SqlCommand(sQuery, oConn);

                Command.CommandType = CommandType.Text;

                Command.Parameters.AddWithValue("@insortby", categorySortBy);
                Command.Parameters.AddWithValue("@categorysetname", "IMPNACCategories");
                var reader = await Command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    categoryModel.Description = reader.GetString(0);
                    categoryModel.Code = reader.GetString(1);
                    categoryModel.IdSiorg = Convert.ToString(reader.GetInt32(2));
                }
            }

            return categoryModel;
        }


    }
}
