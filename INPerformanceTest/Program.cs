using INPerformanceTest.Entity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INPerformanceTest
{
    internal class Program
    {
        static void Main(string[] args)
        {

            var sql = @"/****** Script for SelectTopNRows command from SSMS  ******/
SELECT TOP (1000)[s_id]
      ,[s_INArticleType]
      ,[s_INArticleStatus]
      ,[s_INColumnSize]
      ,[s_INRejected]
      ,[s_INAutoPaginated]
      ,[s_INRootCategory]
      ,[s_INCategory]
      ,[s_INSortBy]
      ,[s_INColumnSizeName]
      ,[s_INArticleSubType]
      ,[s_INMateriaSeq]
      ,[s_INAssignedUserRef]
      ,[s_INMateriaType]
      ,[s_INHighlight]
      ,[s_INSection]
      ,[s_INHighlightUser]
      ,[s_INHasHighlight]
      ,[s_duration]
      ,[s_INOficioId]
      ,[s_INMateriaId]
      ,[s_INMateriaTypeId]
      ,[s_INHighlightPriority]
      ,[s_INParentId]
      ,[s_INChildPosition]
      ,[s_INRelationship]
      ,[s_INTitIdSiorg]
      ,[s_INTitDescription]
      ,[s_INTitCode]
  FROM [s_ArticleTable] WHERE s_id = @articleId";


            string myConnectionString = ConfigurationManager.AppSettings["ConnectionString"];


            using (var connection = new SqlConnection(myConnectionString))
            using (var command = new SqlCommand(sql, connection))
            {
                connection.Open();
                command.Parameters.AddWithValue("@articleId", 23588016);

                using (var reader = command.ExecuteReader())
                {

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {

                            var article = new INArticle()
                            {
                                s_id = (int)reader["s_id"],
                            };

                        }

                    }



                }
            }


        }
    }
}

