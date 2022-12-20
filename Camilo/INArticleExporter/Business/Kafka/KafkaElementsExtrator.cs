using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Imprensa.Business.Entities;
using System.Xml.Linq;
using INPerformanceTest.Business.Interfaces;
using Microsoft.Extensions.Logging;
using ILoggerFactory = INPerformanceTest.Interfaces.ILoggerFactory;
using INPerformanceTest.Business.Helpers;

namespace Imprensa
{
    namespace Business
    {
        public class KafkaDocumentExtrator : IKafkaDocumentExtrator
        {
            private List<PageArticle> pageArticles;
            private List<string> processedArticles = new List<string>();
            private List<KafkaArticleDocument> kafkaArticleDocuments;
            private ILogger exporterLogger;
            private readonly ITitleHandlerFactory titleHandlerFactory;
            private readonly ICategoryService categoryService;
            private readonly IBase64Handler base64Handler;
            private readonly IKafkaService kafkaService;
            private readonly ILoggerFactory loggerFactory;
            private readonly IKafkaArticleXmlImgHandler kafkaArticleXmlImgHandler;
            private readonly IPublicationInfo publicationInfo;

            public KafkaDocumentExtrator(ITitleHandlerFactory titleHandlerFactory, ICategoryService categoryService, IBase64Handler base64Handler, IKafkaService kafkaService, ILoggerFactory loggerFactory, IKafkaArticleXmlImgHandler kafkaArticleXmlImgHandler, IPublicationInfo publicationInfo)
            {
                exporterLogger = loggerFactory.CreateLogger("ExportXml");
                this.titleHandlerFactory = titleHandlerFactory;
                this.categoryService = categoryService;
                this.base64Handler = base64Handler;
                this.kafkaService = kafkaService;
                this.loggerFactory = loggerFactory;
                this.kafkaArticleXmlImgHandler = kafkaArticleXmlImgHandler;
                this.publicationInfo = publicationInfo;
            }
            public async Task<List<KafkaArticleDocument>> GetKafkaDocuments(List<PageArticle> pageArticles)
            {
                if (pageArticles.Count == 0)
                {
                    throw new Exception("Could not load because Edition is empty");
                }

                var listTask = new List<Task<KafkaArticleDocument>>();
                pageArticles = (from a in pageArticles
                                orderby a.Article.INSortBy, a.Article.Name
                                select a).ToList();
                var articles = (from a in pageArticles select a.Article).ToList();

                var kafkaArticleDocuments = new List<KafkaArticleDocument>();
              
                var titileHandler = await titleHandlerFactory.GetTitHandler(articles);

                foreach (var pageArticle in pageArticles)
                {
                    bool isTit = pageArticle.Article.INMateriaId == "0";
                    if (isTit)
                        continue;

                    var task =  GetKafkaArticleDocument(pageArticle, titileHandler);
                    
                    listTask.Add(task);
                    if (listTask.Count == 5)
                    {
                        var result = await Task.WhenAll(listTask.ToArray());
                        kafkaArticleDocuments.AddRange(result);

                        listTask = new List<Task<KafkaArticleDocument>>();
                    }


                }
               // var result = await Task.WhenAll(listTask.ToArray());
                
                return kafkaArticleDocuments;
            }

            private async Task<KafkaArticleDocument> GetKafkaArticleDocument(PageArticle pageArticle, ITitleHandler titleHandler)
            {
                var doc = await FetchKafkaDocument(pageArticle);
                if (doc == null)
                {
                    throw new Exception("KafkaDocument is null");
                }

                var kafkaArticleDoc = await CreateKafkaArticleDocument(pageArticle, doc, titleHandler);
                return kafkaArticleDoc;
            }

            private Task<XDocument> FetchKafkaDocument(PageArticle pageArticle)
            {
                string publication =  publicationInfo.TitleName;

                return kafkaService.LoadKafkaAsync(pageArticle.Article.Id.ToString(), publication);
            }
            private async Task<KafkaArticleDocument> CreateKafkaArticleDocument(PageArticle pageArticle, XDocument xdoc, ITitleHandler titleHandler)
            {
                try
                {
                    var article = pageArticle.Article;

                    exporterLogger.LogInformation($"INArticleID: {article.INMateriaId}");

                    var pagenumber = pageArticle.PageNum;

                    var pubDate = new XAttribute("pubDate", pageArticle.EditionModel.PublicationDate.ToString("dd/MM/yyyy"));
                    var artClass = new XAttribute("artClass", article.INSortBy);
                    var pubName = new XAttribute("pubName", publicationInfo.TitleName);
                    var idOficio = new XAttribute("idOficio", article.INOficioId);
                    var artType = new XAttribute("artType", article.INMateriaType);
                    var INMateriaId = new XAttribute("idMateria", article.INMateriaId);

                    var path = await categoryService.GetCategoryPathAsync(Convert.ToInt32(article.INPortalCategoryId));

                    if ((path.ToLower().Contains("Editais e Avisos".ToLower())))
                    {
                        string edavi = await categoryService.GetEdaviCategoryAsync(article.INSortBy);

                        path = $"{path}/{edavi}";
                    }

                    exporterLogger.LogInformation("Path {0}", path);

                    if ((path.Contains("Ineditoriais/")))
                    {
                        path = path.Replace("Ineditoriais/ASSOCIAÇÕES/", "Ineditoriais/");
                        path = path.Replace("Ineditoriais/FACULDADES/", "Ineditoriais/");
                        path = path.Replace("Ineditoriais/FEDERAÇÕES/", "Ineditoriais/");
                        path = path.Replace("Ineditoriais/FUNDAÇÕES/", "Ineditoriais/");
                        path = path.Replace("Ineditoriais/SINDICATOS/", "Ineditoriais/");
                    }

                    var artSection = new XAttribute("artCategory", path);

                    var editionNumber = new XAttribute("editionNumber", pageArticle.EditionModel.INEditionNumber);

                    var splitted = article.INHighLight.Split('|');

                    var highlightType = new XAttribute("highlightType", "");
                    var highlightPriority = new XAttribute("highlightPriority", "");
                    var highlight = new XAttribute("highlight", "");
                    var filenameimg = "";
                    var highlightimageName = new XAttribute("highlightimagename", "");
                    var highlightImage = new XAttribute("highlightimage", "");

                    if (splitted.Length == 3)
                    {
                        highlightType = new XAttribute("highlightType", splitted[0]);
                        highlightPriority = new XAttribute("highlightPriority", splitted[1]);
                        highlight = new XAttribute("highlight", splitted[2]);
                    }
                    if (splitted.Length == 4)
                    {
                        highlightType = new XAttribute("highlightType", splitted[0]);
                        highlightPriority = new XAttribute("highlightPriority", splitted[1]);
                        highlight = new XAttribute("highlight", splitted[2]);

                        filenameimg = splitted[3];
                        var filebitmap = "";
                        if ((!string.IsNullOrEmpty(filenameimg)))
                        {
                            var base64img = base64Handler.ConvertToBase64(filenameimg);

                            filebitmap = string.Format("data:image/jpeg;base64,{0}", base64img);
                        }

                        highlightImage = new XAttribute("highlightimage", filebitmap);
                        highlightimageName = new XAttribute("highlightimagename", filenameimg);
                    }

                    var artSize = new XAttribute("artSize", article.INColumnSizeName);
                    var artNotes = new XAttribute("artNotes", article.Notes);

                    var pdfs = new List<string>();

                    // Dim webTitle = GSIUtils.GetIdJornal(kafkaArticle.EditionProperties.TitleName, kafkaArticle.EditionModel.INEditionNumber, kafkaArticle.EditionModel.Name)
                    var webTitle = pageArticle.EditionModel.webEditionNumber;
                    var numberPage = new XAttribute("numberPage", pagenumber);

                    var pdfPage = new XAttribute("pdfPage", string.Format("http://pesquisa.in.gov.br/imprensa/jsp/visualiza/index.jsp?data={0}&jornal={1}&pagina={2}", pageArticle.EditionModel.PublicationDate.ToString("dd/MM/yyyy"), webTitle, pagenumber));

                    var jornal = "Diário Oficial da União";

                    if ((publicationInfo.TitleName.ToLower().Contains("dodf")))
                    {
                        jornal = "Diário Oficial Do Distrito Federal";
                        var dodfPubDate = pageArticle.EditionModel.PublicationDate;

                        var dateInPortuguese = dodfPubDate.ToString("M");
                        var day = dodfPubDate.ToString("dd");
                        var splittedDate = dateInPortuguese.Split(' ');

                        var diames = $"{day}_{splittedDate.Last()}";
                        var ano = dodfPubDate.ToString("yyy");
                        var customdate = dodfPubDate.ToString("dd-MM-yyyy");

                        var pdfname = "INTEGRA";

                        var INEditionNumber = pageArticle.EditionModel.INEditionNumber;

                        var url = $"http://www.buriti.df.gov.br/ftp/diariooficial/{ano}/{diames}/DODF {INEditionNumber} {customdate}/&arquivo=DODF {INEditionNumber} {customdate} {pdfname}.pdf";

                        var dodfpubName = "DODF1";

                        if (article.INSortBy.StartsWith("2."))
                            dodfpubName = "DODF2";

                        if (article.INSortBy.StartsWith("3."))
                            dodfpubName = "DODF3";

                        if (publicationInfo.TitleName.Contains("DODFE"))
                        {
                            pdfname = "EDICAO EXTRA";

                            url = $"http://www.buriti.df.gov.br/ftp/diariooficial/{ano}/{diames}/DODF {INEditionNumber} {customdate} {pdfname}/&arquivo=DODF {INEditionNumber} {customdate} {pdfname}.pdf";

                            dodfpubName = publicationInfo.TitleName;
                        }

                        if (publicationInfo.TitleName.Contains("DODFS"))
                        {
                            pdfname = "SUPLEMENTO";

                            url = $"http://www.buriti.df.gov.br/ftp/diariooficial/{ano}/{diames}/DODF {INEditionNumber} {customdate} {pdfname}/&arquivo=DODF {INEditionNumber} {customdate} {pdfname}.pdf";

                            dodfpubName = publicationInfo.TitleName;
                        }

                        url = Uri.EscapeUriString(url);
                        pdfPage = new XAttribute("pdfPage", url);
                        pubName = new XAttribute("pubName", dodfpubName);
                    }

                    var XSortyBy = xdoc.Descendants().SingleOrDefault(p => p.Name.LocalName == "ordem");
                    XSortyBy.Value = artClass.Value;

                    var XArtId = xdoc.Descendants().SingleOrDefault(p => p.Name.LocalName == "idMateria");
                    XArtId.Value = INMateriaId.Value;

                    var XArttype = xdoc.Descendants().SingleOrDefault(p => p.Name.LocalName == "tipoAto");
                    XArttype.Value = article.INMateriaType;

                    var XArttypeId = xdoc.Descendants().SingleOrDefault(p => p.Name.LocalName == "idTipoAto");
                    XArttypeId.Value = article.INMateriaTypeId;

                    var romanYear = Utils.ToRoman(pageArticle.EditionModel.PublicationDate.Year);

                    var XYear = xdoc.Descendants().SingleOrDefault(p => p.Name.LocalName == "ano");
                    XYear.Value = pageArticle.EditionModel.INEditionYear;

                    var XIdJornal = xdoc.Descendants().SingleOrDefault(p => p.Name.LocalName == "idJornal");
                    XIdJornal.Value = webTitle;

                    var XisExtra = xdoc.Descendants().SingleOrDefault(p => p.Name.LocalName == "isExtra");
                    XisExtra.Value = (publicationInfo.isExtra).ToString();

                    var XisSuplemento = xdoc.Descendants().SingleOrDefault(p => p.Name.LocalName == "isSuplemento");
                    XisSuplemento.Value = publicationInfo.IsSuplement.ToString();

                    var XJornal = xdoc.Element("materia").Element("metadados").Element("publicacao").Element("jornal").Descendants().SingleOrDefault(p => p.Name.LocalName == "jornal");
                    XJornal.Value = jornal;

                    var XEditionNumber = xdoc.Descendants().SingleOrDefault(p => p.Name.LocalName == "numeroEdicao");
                    XEditionNumber.Value = editionNumber.Value;

                    var SectionNumber = Regex.Match(publicationInfo.TitleName, @"\d+").Value;

                    var XSectionId = xdoc.Descendants().SingleOrDefault(p => p.Name.LocalName == "idSecao");
                    XSectionId.Value = SectionNumber;

                    var XSection = xdoc.Descendants().SingleOrDefault(p => p.Name.LocalName == "secao");
                    XSection.Value = "Seção " + SectionNumber;

                    var XIdOficio = xdoc.Descendants().SingleOrDefault(p => p.Name.LocalName == "idOficio");
                    XIdOficio.Value = article.INOficioId;

                    var XidXML = xdoc.Descendants().SingleOrDefault(p => p.Name.LocalName == "idXML");
                    XidXML.Value = string.Format("{0}+{1}+{2}", webTitle, pageArticle.EditionModel.PublicationDate.ToString("yyyy-MM-dd"), article.INMateriaId);

                    var XPubdate = xdoc.Descendants().SingleOrDefault(p => p.Name.LocalName == "dataPublicacao");
                    XPubdate.Value = pageArticle.EditionModel.PublicationDate.ToString("yyyy-MM-dd");

                    var XPagNum = xdoc.Descendants().SingleOrDefault(p => p.Name.LocalName == "numeroPaginaPdf");
                    XPagNum.Value = numberPage.Value;

                    var XHilightType = xdoc.Descendants().SingleOrDefault(p => p.Name.LocalName == "tipo");
                    XHilightType.Value = highlightType.Value;

                    var XUrl = xdoc.Descendants().SingleOrDefault(p => p.Name.LocalName == "urlVersaoOficialPdf");
                    XUrl.Value = pdfPage.Value;

                    var metadados = xdoc.Element("materia").Element("metadados");

                    if (!string.IsNullOrEmpty(highlight.Value))
                    {
                        var XHighlightPriority = xdoc.Descendants().SingleOrDefault(p => p.Name.LocalName == "prioridade");
                        XHighlightPriority.Value = highlightPriority.Value;

                        var XHighlightImage = xdoc.Element("materia").Element("metadados").Element("destaque").Descendants().SingleOrDefault(p => p.Name.LocalName == "fonteImagem");
                        XHighlightImage.Value = highlightImage.Value;

                        var xHighlightImageName = xdoc.Element("materia").Element("metadados").Element("destaque").Descendants().SingleOrDefault(p => p.Name.LocalName == "nomeImagem");
                        xHighlightImageName.Value = filenameimg;

                        var XHighlightTexto = xdoc.Element("materia").Element("metadados").Element("destaque").Descendants().SingleOrDefault(p => p.Name.LocalName == "texto");

                        XHighlightTexto.Value = highlight.Value;
                    }
                    else
                    {
                        var destaque = xdoc.Element("materia").Element("metadados").Element("destaque");
                        destaque.RemoveNodes();
                    }

                    var texto = xdoc.Element("materia").Element("texto");

                    var origem = xdoc.Element("materia").Element("metadados").Element("origem");
                    exporterLogger.LogInformation("Getting categoryTree...");                    

                    var categoryTree =await titleHandler.GetOrigemXmlElement(article);

                    origem.ReplaceWith(categoryTree);

                    var materiasComposicaoTexto = xdoc.Element("materia").Element("metadados").Element("materiasComposicaoTexto");

                    if ((materiasComposicaoTexto == null))
                        throw new Exception("materiasComposicaoTexto is missing");

                    var newmateriasComposicaoTexto = new XElement("materiasComposicaoTexto");
                    var MateriaComposicaoidMateria = new XElement("idMateria", article.INMateriaId);
                    var MateriaComposicaoidOficio = new XElement("idOficio", article.INOficioId);
                    var MateriaComposicaoOrdem = new XElement("ordem", null);

                    newmateriasComposicaoTexto.Add(MateriaComposicaoidMateria);
                    newmateriasComposicaoTexto.Add(MateriaComposicaoidOficio);
                    newmateriasComposicaoTexto.Add(MateriaComposicaoOrdem);
                    materiasComposicaoTexto.ReplaceWith(newmateriasComposicaoTexto);
                    ReplaceCharacter(xdoc);

                    exporterLogger.LogInformation("Add Base64 to Images...");
                    kafkaArticleXmlImgHandler.AddBase64Img(xdoc);
                    return new KafkaArticleDocument()
                    {
                        KafkaXDocument = xdoc,
                        PageArticle = pageArticle
                    };
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
            private void ReplaceCharacter(XDocument xdoc)
            {
                if (xdoc.ToString().IndexOf((char)128) != -1)
                {
                    xdoc.Element("materia").Element("textoHTML").SetValue(Regex.Replace(xdoc.Element("materia").Element("textoHTML").Value, $"{((char)128).ToString()}.?DELTAREPLACE", "&#916;&nbsp;"));
                    xdoc.Element("materia").Element("textoHTML").SetValue(Regex.Replace(xdoc.Element("materia").Element("textoHTML").Value, $"{((char)128).ToString()}.?GTEQUALREPLACE", "&#8805;&nbsp;"));
                    xdoc.Element("materia").Element("textoHTML").SetValue(Regex.Replace(xdoc.Element("materia").Element("textoHTML").Value, $"{((char)128).ToString()}.?LTEQUALREPLACE", "&#8804;&nbsp;"));
                }
                else
                {
                    xdoc.Element("materia").Element("textoHTML").SetValue(xdoc.Element("materia").Element("textoHTML").Value.Replace("DELTAREPLACE", "&#916;&nbsp;"));
                    xdoc.Element("materia").Element("textoHTML").SetValue(xdoc.Element("materia").Element("textoHTML").Value.Replace("GTEQUALREPLACE", "&#8805;&nbsp;"));
                    xdoc.Element("materia").Element("textoHTML").SetValue(xdoc.Element("materia").Element("textoHTML").Value.Replace("LTEQUALREPLACE", "&#8804;&nbsp;"));
                }
            }

        }
    }
}
