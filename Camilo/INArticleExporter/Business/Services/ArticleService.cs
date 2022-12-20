using Imprensa.Business;
using INPerformanceTest.Business.Entities.Models;
using INPerformanceTest.Entity;
using INPerformanceTest.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace INPerformanceTest.Business.Services
{
    public class ArticleService : IArticleService
    {
        private readonly IApiService apiService;
        private readonly ICredentials credentials;
        private readonly ILogger logger;

        public ArticleService(IApiService apiService, ICredentials credentials, ILogger logger)
        {
            this.apiService = apiService;
            this.credentials = credentials;
            this.logger = logger;
        }

        public async Task<List<ArticleModel>> GetArticlesByPageAsync(int pageId, bool isWebXml)
        {
            var articles = new List<ArticleModel>();

            try
            {
                // Dim strDoUrl = String.Format("{0}/do.ashx?ticket={1}&cmd=search&name=gn4:article[gn4:pageLayers/gn4:item/gn4:pageRef[@idref='{2}']  and @INArticleStatus='PageReady']&feed={3}",
                var strDoUrl = string.Format("{0}/do.ashx?ticket={1}&cmd=search&name=gn4:article[@INArticleStatus='PageReady' and gn4:pageLayers/gn4:item/gn4:pageRef[@idref='{2}']]&feed={3}", credentials.Url, credentials.Ticket, $"obj{pageId}", "GSIArticleModel");
                if (isWebXml)
                    strDoUrl = string.Format("{0}/do.ashx?ticket={1}&cmd=search&name=gn4:article[gn4:pageLayers/gn4:item/gn4:pageRef[@idref='{2}']]&feed={3}", credentials.Url, credentials.Ticket, $"obj{pageId}", "GSIArticleModel");

                XDocument xDoc = await apiService.LoadXDocumentAsync(strDoUrl);
                if (xDoc != null)
                {
                    XNamespace gn4 = "urn:schemas-teradp-com:gn4tera";
                    var xArticles = xDoc.Descendants("article");
                    foreach (XElement xArticle in xArticles)
                    {
                        var articleId = Int32.Parse(xArticle.Attribute("id").Value);
                        var name = xArticle.Attribute("name").Value;
                        var inSortBy = xArticle.Attribute("INSortBy").Value;
                        var inRootCategory = xArticle.Attribute("INRootCategory").Value;
                        var inCategory = xArticle.Attribute("INCategory").Value;

                        logger.LogInformation($"Loading Article: {articleId}");

                        var inPortalCategoryId = "";
                        try
                        {
                            inPortalCategoryId = xArticle.Attribute("INPortalCategoryId").Value;
                        }
                        catch (Exception ex)
                        {
                        }

                        var inCategoryDescription = xArticle.Attribute("INCategoryDescription").Value;
                        var inMateriaId = "";
                        try
                        {
                            inMateriaId = xArticle.Attribute("INMateriaId").Value;
                        }
                        catch (Exception ex)
                        {
                        }

                        var inOficioId = "";

                        try
                        {
                            inOficioId = xArticle.Attribute("INOficioId").Value;
                        }
                        catch (Exception ex)
                        {
                        }

                        var inMateriaType = "";
                        try
                        {
                            inMateriaType = xArticle.Attribute("INMateriaType").Value;
                        }
                        catch (Exception ex)
                        {
                        }

                        var inMateriaTypeId = "";
                        try
                        {
                            inMateriaTypeId = xArticle.Attribute("INMateriaTypeId").Value;
                        }
                        catch (Exception ex)
                        {
                        }
                        var inMateriaSeq = "";
                        try
                        {
                            inMateriaSeq = xArticle.Attribute("INMateriaSeq").Value;
                        }
                        catch (Exception ex)
                        {
                        }

                        var iNParentId = "0";
                        try
                        {
                            iNParentId = xArticle.Attribute("INParentId").Value;
                        }
                        catch
                        {
                        }
                        var inCategoryPath = string.Empty;
                        var iNChildPosition = "0";
                        try
                        {
                            iNChildPosition = xArticle.Attribute("INChildPosition").Value;
                        }
                        catch
                        {
                        }
                        if ((iNChildPosition == ""))
                            iNChildPosition = "0";


                        // adding new attributes 
                        var title = xArticle.Attribute("Title")?.Value;
                        var iNColumnSize = "";

                        try
                        {
                            iNColumnSize = xArticle.Attribute("INColumnSize").Value;
                        }
                        catch (Exception ex)
                        {
                        }
                        var inColumnSizeName = "";
                        try
                        {
                            inColumnSizeName = xArticle.Attribute("INColumnSizeName").Value;
                        }
                        catch (Exception ex)
                        {
                        }

                        var bodyTextH = "";

                        try
                        {
                            bodyTextH = xArticle.Attribute("bodyTextH").Value;
                        }
                        catch
                        {
                        }

                        var inArticleStatus = "";
                        try
                        {
                            inArticleStatus = xArticle.Attribute("INArticleStatus").Value;
                        }
                        catch
                        {
                        }

                        var iNArticleSubType = xArticle.Attribute("INArticleSubType").Value;

                        try
                        {
                            iNArticleSubType = xArticle.Attribute("INArticleSubType").Value;
                        }
                        catch
                        {
                        }

                        var iNTitIdSiorg = "";
                        var iNTitDescription = "";
                        var iNTitCode = "";
                        if (inMateriaId == "0")
                        {
                            try
                            {
                                iNTitIdSiorg = xArticle.Attribute("INTitIdSiorg").Value;
                            }
                            catch
                            {
                            }
                            try
                            {
                                iNTitDescription = xArticle.Attribute("INTitDescription").Value;
                            }
                            catch
                            {
                            }
                            try
                            {
                                iNTitCode = xArticle.Attribute("INTitCode").Value;
                            }
                            catch
                            {
                            }
                        }

                        var xfolder = xArticle.Descendants("folder").FirstOrDefault();
                        var folder = string.Empty;
                        if (xfolder != null)
                            folder = xfolder.Value;

                        if (xArticle.Attribute("INCategoryPath") != null)
                            inCategoryPath = xArticle.Attribute("INCategoryPath").Value;

                        var pageNumbers = new List<int>();
                        var xpages = xArticle.Descendants("page");
                        foreach (XElement xpage in xpages)
                        {
                            var number = Convert.ToInt32(xpage.LastAttribute.Value);
                            pageNumbers.Add(number);
                        }

                        var xINHightlight = xArticle.Descendants("INHighlight").FirstOrDefault();
                        var INHightlight = string.Empty;
                        if (xINHightlight != null)
                            INHightlight = xINHightlight.Value;

                        var xnotes = xArticle.Descendants("notes").FirstOrDefault();
                        var notes = string.Empty;
                        if (xnotes != null)
                            notes = xnotes.Value;

                        var article = new ArticleModel(articleId);
                        {
                            var withBlock = article;
                            withBlock.Name = name;
                            withBlock.INSortBy = inSortBy;
                            withBlock.INRootCategoryId = inRootCategory;
                            withBlock.INCategoryId = inCategory;
                            withBlock.INPortalCategoryId = inPortalCategoryId;
                            withBlock.INCategoryPath = inCategoryPath;
                            withBlock.INMateriaSeq = inMateriaSeq;
                            withBlock.INCategoryDescription = inCategoryDescription;
                            withBlock.INMateriaId = inMateriaId;
                            withBlock.INMateriaType = inMateriaType;
                            withBlock.INMateriaTypeId = inMateriaTypeId;
                            withBlock.INOficioId = inOficioId;
                            withBlock.INColumnSizeName = inColumnSizeName;
                            withBlock.PageNumbers = pageNumbers;
                            withBlock.INHighLight = INHightlight;
                            withBlock.Notes = notes;
                            withBlock.INArticleStatus = inArticleStatus;
                            withBlock.INArticleSubType = iNArticleSubType;
                            withBlock.INColumnSize = iNColumnSize;
                            withBlock.INColumnSizeName = inColumnSizeName;
                            withBlock.bodyTextH = bodyTextH;
                            withBlock.Title = title;
                            withBlock.INChildPosition = iNChildPosition;
                            withBlock.INParentId = iNParentId;
                            withBlock.INTitDescription = iNTitDescription;
                            withBlock.INTitIdSiorg = iNTitIdSiorg;
                            withBlock.INTitCode = iNTitCode;
                        }

                        var xLayers = xArticle.Descendants("layers");
                        foreach (XElement xLayer in xLayers)
                        {
                            var xContents = xLayer.Descendants("e");
                            foreach (XElement xContent in xContents)
                            {
                                var content = new LayerContentModel(Int32.Parse(xContent.Attribute("txtLI").Value));
                                {
                                    var withBlock = content;
                                    withBlock.ContentType = xContent.Attribute("type").Value;
                                }

                                if ((content.ContentType.ToLower() == "photocaption"))
                                {
                                    content.MediaId = Int32.Parse(xContent.Attribute("media").Value);
                                    content.Name = xContent.Attribute("name").Value;
                                }

                                article.Contents.Add(content);
                            }
                        }

                        logger.LogInformation($"Finshed Loading Article: {articleId}");
                        articles.Add(article);
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Load articles from api error");
            }

            return articles;
        }

    }
}
