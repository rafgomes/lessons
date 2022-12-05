using INPerformanceTest.Entity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using INPerformanceTest.Shared.Log;
using INPerformanceTest.Commons;
using System.Security.Policy;
using System.Xml.Linq;
using System.Diagnostics;
using INPerformanceTest.Shared;

namespace INPerformanceTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if(args.Length == 0)
            {
                Console.WriteLine("Por favor, entre com o ID da matéria do GN4");
                return;
            }

            string idMateriaGN = args[0];

            string logFolder = WebConfigHelper.LogFolder();

            ILogger logger = Logger.GetLogger(logFolder, logToConsole: true);

            try
            {
                logger.LogInformation("Inicio ao teste de Performance");

                logger.LogInformation("Fazendo Loggin no GN4...");
                SessionInfo session = Gn4Helper.GN4Session;

                logger.LogInformation($"Loggado: {session.Ticket}");
                Stopwatch stopwatchstopwatch = Stopwatch.StartNew();

                int numberOfTry = WebConfigHelper.GetNumberOfTry();

                logger.LogInformation($"Fazendo teste com busca de {numberOfTry} articles usando apenas request de API nativa do GN4");
                for (int i = 0; i < numberOfTry; i++)
                {
                    ReadArticleFromApi(idMateriaGN);
                }
                stopwatchstopwatch.Stop();

                logger.LogInformation($"Tempo total API: {stopwatchstopwatch.Elapsed.TotalMilliseconds} milesegundos; Total: {stopwatchstopwatch.Elapsed.TotalSeconds} segundos");

                stopwatchstopwatch.Restart();                                       

                logger.LogInformation($"Fazendo teste com busca de {numberOfTry} articles usando apenas SQL");
                
                for (int i = 0; i < numberOfTry; i++)
                {
                    ReadArticleFromDb(idMateriaGN);
                }
                stopwatchstopwatch.Stop();

                logger.LogInformation($"Tempo total DB: {stopwatchstopwatch.Elapsed.TotalMilliseconds} milesegundos; Total: {stopwatchstopwatch.Elapsed.TotalSeconds} segundos");
                       
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                Console.WriteLine(ex.ToString());
            }

            Console.ReadLine();

        }
        static void ReadArticleFromApi(string idMateriaGN)
        {

            SessionInfo loginData = Gn4Helper.GN4Session;

            string urlRequest = $@"{loginData.Url}/do.ashx?cmd=search&name=gn4:article[@id=%22obj{idMateriaGN}%22]&feed=GSIArticleModel&ticket={loginData.Ticket}";

            XDocument xDoc = XDocument.Load(urlRequest);
            XNamespace gn4Namesapce = "urn:schemas-teradp-com:gn4tera";

            IEnumerable<XElement> xArticles = xDoc.Descendants("article");

            /***
             * <article id="23588016" name="4024 PortariaPropostaFAF_20211229_162658902" 
             * INSortBy="00026:00001:00000:00000:00000:00000:00000:00000:00000:00000:00054:00003" 
             * INRelationship="" INParentId=""
             * INChildPosition="" 
             * INMateriaId="14147033" 
             * INRootCategory="" 
             * INCategory="22077826"
             * INCategoryDescription="Gabinete do Ministro" 
             * INPortalCategoryId="186112" 
             * INOficioId="6858100" 
             * INMateriaType="Portaria" 
             * INMateriaTypeId="69"
             * INCategoryPath="/MS/Gabinete do Ministro" 
             * INMateriaSeq="0" 
             * INArticleStatus="PageReady" 
             * INArticleSubType="Content"
             * INColumnSizeName="24" 
             * INColumnSize="1" 
             * INTitIdSiorg="0"
             * INTitDescription=""
             * INTitCode="0"
             * bodyTextH="594600">
...
</article>*/

            foreach (XElement xArticle in xArticles)
            {

                string articleId = xArticle.Attribute("id").Value;
                string name = xArticle.Attribute("name").Value;

                var inArticle = new INArticle();
                inArticle.s_id = Convert.ToInt32(xArticle.Attribute("id").Value);
                inArticle.s_INMateriaId = Convert.ToInt32(xArticle.Attribute("INMateriaId").Value);
                inArticle.s_INSortBy = xArticle.Attribute("INSortBy").Value;
                inArticle.s_INRelationship = xArticle.Attribute("INRelationship").Value;
                inArticle.s_INChildPosition = xArticle.Attribute("INChildPosition").Value;
                //inArticle.s_INRootCategory = Convert.ToInt32(xArticle.Attribute("INRootCategory").Value);
                inArticle.s_INCategory = Convert.ToInt32(xArticle.Attribute("INCategory").Value);
                inArticle.s_INOficioId = Convert.ToInt32(xArticle.Attribute("INOficioId").Value);
                inArticle.s_INMateriaType = xArticle.Attribute("INMateriaType").Value;
                inArticle.s_INMateriaTypeId = Convert.ToInt32(xArticle.Attribute("INMateriaTypeId").Value);
                inArticle.s_INMateriaSeq = Convert.ToInt32(xArticle.Attribute("INMateriaSeq").Value);
                inArticle.s_INArticleStatus = xArticle.Attribute("INArticleStatus").Value;
                inArticle.s_INArticleSubType = xArticle.Attribute("INArticleStatus").Value;
                inArticle.s_INColumnSizeName = Convert.ToInt32(xArticle.Attribute("INColumnSizeName").Value);
                inArticle.s_INColumnSize = Convert.ToInt32(xArticle.Attribute("INColumnSize").Value);
                inArticle.s_INTitIdSiorg = Convert.ToInt32(xArticle.Attribute("INTitIdSiorg").Value);
                inArticle.s_INTitDescription = xArticle.Attribute("INTitDescription").Value;
                inArticle.s_INTitCode = Convert.ToInt32(xArticle.Attribute("INTitCode").Value);
                inArticle.s_bodyTextH = Convert.ToInt32(xArticle.Attribute("bodyTextH").Value);
            }

        }
        static void ReadArticleFromDb(string idMateriaGN)
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
                command.Parameters.AddWithValue("@articleId", Convert.ToInt32(idMateriaGN));

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
                                s_INArticleStatus = Gn4Helper.GetINSatus((short)reader["s_INArticleStatus"]),
                                s_INColumnSize = (int)reader["s_INColumnSize"],
                                s_INRejected = (short)reader["s_INRejected"],
                                s_INAutoPaginated = (short)reader["s_INAutoPaginated"],
                                s_INRootCategory = (int)reader["s_INRootCategory"],
                                s_INCategory = (int)reader["s_INCategory"],
                                s_INSortBy = (string)reader["s_INSortBy"],
                                s_INColumnSizeName = (int)reader["s_INColumnSizeName"],
                                s_INArticleSubType = Gn4Helper.GetINSubType((short)reader["s_INArticleSubType"]),
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

