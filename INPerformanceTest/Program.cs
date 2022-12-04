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
                                s_INArticleType = (short)reader["s_INArticleType"],
                                s_INArticleStatus = (short)reader["s_INArticleStatus"],
                                s_INColumnSize = (int)reader["s_INColumnSize"],
                                s_INRejected = (short)reader["s_INRejected"],
                                s_INAutoPaginated = (short)reader["s_INAutoPaginated"],
                                s_INRootCategory = (int)reader["s_INRootCategory"],
                                s_INCategory = (int)reader["s_INCategory"],
                                s_INSortBy = (string)reader["s_INSortBy"],
                                s_INColumnSizeName = (int)reader["s_INColumnSizeName"],
                                s_INArticleSubType = (short)reader["s_INArticleSubType"],
                                s_INMateriaSeq = (int)reader["s_INMateriaSeq"],
                                s_INAssignedUserRef = (int)reader["s_INAssignedUserRef"],
                                s_INMateriaType = (string)reader["s_INMateriaType"],
                                s_INHighlight = (string)reader["s_INHighlight"],
                                s_INSection = (string)reader["s_INSection"],
                                s_INHighlightUser = (string)reader["s_INHighlightUser"],
                                s_INHasHighlight = (short)reader["s_INHasHighlight"],
                                //s_duration = 
                                s_INOficioId = (int)reader["s_INOficioId"],
                                s_INMateriaId = (int)reader["s_INMateriaId"],
                                s_INMateriaTypeId = (int)reader["s_INMateriaTypeId"],
                                s_INHighlightPriority = (int)reader["s_INHighlightPriority"],
                                s_INParentId = (string)reader["s_INParentId"],
                                s_INChildPosition = (string)reader["s_INChildPosition"],
                                s_INRelationship = (string)reader["s_INRelationship"],
                                s_INTitIdSiorg = (int)reader["s_INTitIdSiorg"],
                                s_INTitDescription = (string)reader["s_INTitDescription"],
                                s_INTitCode = (int)reader["s_INTitCode"]
                            };

                        }

                    }



                }
            }


        }
    }
}

