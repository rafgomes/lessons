using Confluent.Kafka;
using INPerformanceTest.Business.Entities.Models;
using INPerformanceTest.Business.Interfaces;
using INPerformanceTest.Business.Services;
using INPerformanceTest.Extensions;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INPerformanceTest.Business.Repositories
{
    public class EditionRepository : IEditionRepository
    {
        private readonly IConfig config;
        private readonly ILogger logger;

        public EditionRepository(IConfig config, ILogger logger) {
            ExpressionExtensions.CheckForNullArgument(() => config, ()=> logger);
            this.config = config;
            this.logger = logger;
        }

        public async Task<List<EditionModel>> GetGroupEditionsById(int editionId)
        {
            var editions = new List<EditionModel>();

            try
            {
                var sql = @"SELECT s_EditionTable.[s_id] as Id
                  ,obj.s_name as Name
                  ,[s_date] AS PublicationDate
                  ,s_EditionnumberTable.s_description AS webEditionNumber
                  ,s_EditionTable.[s_titleRef] as TitleId     
                  ,[s_INGroup]  as INGroup
                  ,[s_INEditionYear] as INEditionYear
                  ,[s_INEditionNumber] as INEditionNumber
               FROM s_EditionTable

               INNER JOIN s_EditionnumberTable ON s_EditionTable.s_editionNumberRef = s_EditionnumberTable.s_id
               INNER JOIN s_ObjectTable obj on s_EditionTable.s_id = obj.s_id

              where s_INGroup  IN (SELECT s_INGroup FROM s_EditionTable WHERE s_id = @editionId)";

                string myConnectionString = config.ConnectionString;

                using (var connection = new SqlConnection(myConnectionString))
                using (var command = new SqlCommand(sql, connection))
                {
                    await connection.OpenAsync();

                    //connection.Open();

                    command.Parameters.AddWithValue("@editionId", editionId);
                    // using (var reader = await command.ExecuteReader())
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (reader.HasRows)
                        {
                            while (await reader.ReadAsync())
                            {
                                var edition = new EditionModel();
                                edition.Id = (int)reader["Id"];
                                edition.Name = (string)reader["Name"];
                                edition.PublicationDate = (DateTime)reader["PublicationDate"];
                                edition.webEditionNumber = (string)reader["webEditionNumber"];
                                edition.TitleId = (int)reader["TitleId"];
                                edition.INGroup = (string)reader["INGroup"];
                                edition.INEditionYear = (string)reader["INEditionYear"];
                                edition.INEditionNumber = (string)reader["INEditionNumber"];
                                editions.Add(edition);

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error while trying to access edition table");
            }

            return editions;
        }

        public async Task<IPublicationInfo> GetPublicationInfoByEditionId(int editionId)
        {
            PublicationInfo publicationInfo = null;

            try
            {
                var sql = @"SELECT  [s_date] AS PublicationDate
                  ,s_EditionnumberTable.s_description AS WebEditionNumber
                  ,objTitle.s_name as TitleName 
                  ,[s_INEditionYear] as INEditionYear
                  ,[s_INEditionNumber] as INEditionNumber
               FROM s_EditionTable edition

               INNER JOIN s_EditionnumberTable ON edition.s_editionNumberRef = s_EditionnumberTable.s_id
			   INNER JOIN s_TitleTable title on edition.s_titleRef = title.s_id
			   INNER JOIN s_ObjectTable objTitle on title.s_id = objTitle.s_id

              where  edition.[s_id] =  @editionId";

                string myConnectionString = config.ConnectionString;

                using (var connection = new SqlConnection(myConnectionString))
                using (var command = new SqlCommand(sql, connection))
                {
                    await connection.OpenAsync();

                    //connection.Open();

                    command.Parameters.AddWithValue("@editionId", editionId);
                    // using (var reader = await command.ExecuteReader())
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (reader.HasRows)
                        {
                            if (await reader.ReadAsync())
                            {
                                publicationInfo = PublicationInfo.CreateInstance((DateTime)reader["PublicationDate"], (string)reader["TitleName"], (string)reader["WebEditionNumber"], (string)reader["INEditionYear"], (string)reader["INEditionNumber"]);                          
                            }
                        }
                        else
                        {
                            throw new Exception("Could not fetch Publication Info");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError("Error while trying to access edition table", ex);                
            }

            return publicationInfo;
        }
   
    }
}
