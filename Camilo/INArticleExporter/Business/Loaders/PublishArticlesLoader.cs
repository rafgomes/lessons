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
using System.Text.RegularExpressions;
using System.Xml;
using Newtonsoft.Json;
using System.Collections.Concurrent;
using Imprensa.Business.Entities;
using Imprensa.Business;
using INPerformanceTest.Business.Interfaces;
using Microsoft.Extensions.Logging;
using Imprensa.Business.Exceptions;
using System.Xml.Linq;
using INPerformanceTest.Business.Services;

public class PublishArticlesLoader : IPublishArticlesLoader
{   
    private readonly ILogger debugLogger;
    private readonly INPerformanceTest.Interfaces.ILoggerFactory loggerFactory;
    private readonly ILogger logger;
    private readonly IJsonValidation jsonValidation;
    private readonly IJsonChangesHandler jsonChangesHandler;
    private readonly IPublicationInfo publicationInfo;
    private List<PublishArticle> result = new List<PublishArticle>();
    private bool hasValidationError = false;
    public PublishArticlesLoader(INPerformanceTest.Interfaces.ILoggerFactory loggerFactory, ILogger logger, IJsonValidation jsonValidation, IJsonChangesHandler jsonChangesHandler, IPublicationInfo publicationInfo)
    {
        this.debugLogger = loggerFactory.CreateLogger("Debug");
        this.loggerFactory = loggerFactory;
        this.logger = logger;
        this.jsonValidation = jsonValidation;
        this.jsonChangesHandler = jsonChangesHandler;
        this.publicationInfo = publicationInfo;
    }
    public async Task<List<PublishArticle>> GetPublishArticles(List<KafkaArticleDocument> kafkadocs)
    {
        var kafkaDoc = kafkadocs.First();
        var pageArticle = kafkaDoc.PageArticle;
        // Dim idJornal = GSIUtils.GetIdJornal(pageArticle.EditionProperties.TitleName, pageArticle.EditionModel.INEditionNumber, pageArticle.EditionModel.Name)
        int idJornal = Convert.ToInt32(pageArticle.EditionModel.webEditionNumber);

        var dataPubEfetiva = pageArticle.EditionModel.PublicationDate.ToString("yyyy-MM-dd");
        var listTask = new List<Task>();

        debugLogger.LogInformation("Processando jsons em paralelo");

        foreach (var kafkaArticle in kafkadocs)
        {
            Task task = ProcessArticle(kafkaArticle, idJornal, dataPubEfetiva);
            await task;
        }

        debugLogger.LogInformation("Paralelo: {0}", result.Count);

        if ((this.hasValidationError))
            throw new Exception("Existe(m) erro(s) de validação. Verifique os logs");

        return this.result;
    }

    private async Task ProcessArticle(KafkaArticleDocument kafkaArticle, int idJornal, string dataPubEfetiva)
    {
        try
        {
            debugLogger.LogInformation("Processing article: {0}", kafkaArticle.PageArticle.Article.Id);

         
            if (publicationInfo.TitleName != "DO1A" & kafkaArticle.PageArticle.Article.HasParent)
                return;

            var xdoc = kafkaArticle.KafkaXDocument;
         
            var node_cdata = xdoc.DescendantNodes().OfType<XCData>().ToList();

            foreach (var node in node_cdata)
            {
                node.Parent.Add(node.Value);
                node.Remove();
            }

            var XmlDocument = new XmlDocument();

            debugLogger.LogInformation("Convert XDocument to String");

            var xDocString = xdoc.ToString();
            // xDocString = xDocString.Replace("<![CDATA[", "").Replace("]]>", "")

            debugLogger.LogInformation("String length: {0}", xDocString.Length);

            XmlDocument.LoadXml(xDocString);
            var timestamp = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");

            debugLogger.LogInformation("Convert XDocument to Json String");
            var jsonString = JsonConvert.SerializeXmlNode(XmlDocument, Newtonsoft.Json.Formatting.Indented);

            debugLogger.LogInformation("jsonString length: {0}", jsonString.Length);
           
            jsonString = jsonChangesHandler.GetJson(jsonString);
            debugLogger.LogInformation("jsonString length after Replace: {0}", jsonString.Length);
            debugLogger.LogInformation("Validation...");
            await jsonValidation.Validate(jsonString);
            var key = string.Format("{0}+{1}+{2}+{3}", idJornal, dataPubEfetiva, kafkaArticle.PageArticle.Article.INMateriaId, timestamp);
            debugLogger.LogInformation("Key: {0}", key);

            var publishArticle = new PublishArticle();

            publishArticle.Article = kafkaArticle.PageArticle.Article;
            publishArticle.Json = jsonString;
            publishArticle.Key = key;

            result.Add(publishArticle);
        }
        catch (InvalidJsonException ex)
        {
            logger.LogWarning(ex.Message);
            if (!this.hasValidationError)
                this.hasValidationError = true;
        }
    }
}
