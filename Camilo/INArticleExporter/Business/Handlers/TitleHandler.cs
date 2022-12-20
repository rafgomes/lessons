using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using Imprensa.Business.Entities;
using Microsoft.Extensions.Logging;
using System.Xml.Linq;
using System.Threading.Tasks;

namespace Imprensa
{
    namespace Business
    {

        public class TitleHandler : ITitleHandler
        {
            private List<ArticleModel> titArticles;
            private readonly ICategoryService categoryService;
            private readonly ILogger logger;
            private List<CategoryModel> mainCategories;

            public TitleHandler(List<CategoryModel> mainCategories, List<ArticleModel> titArticles, ICategoryService categoryService, ILogger logger)
            {
                this.titArticles = titArticles;
                this.categoryService = categoryService;
                this.logger = logger;
                this.mainCategories = mainCategories;
            }
        
            public async Task<XElement> GetOrigemXmlElement(ArticleModel article)
            {
                try
                {
                    var element = new XElement("origem");

                    var titCategories = GetTitCategories(article.INSortBy);

                    titCategories.Reverse();

                    var rootTitCategory = titCategories.FirstOrDefault(a => a.IsRoot == true);

                    if ((rootTitCategory == null))
                        throw new Exception($"Could not get rootTitCategory");

                    var titCategoryChildren = (from a in titCategories
                                               where a.IsRoot == false
                                               select a).ToList();

                    var tempElem = element;
                    var index = 0;

                    foreach (var titCategory in titCategories)
                    {
                        if (titCategory.TitCode == "0")
                            throw new Exception(string.Format("Something wrong with category from article {0}", article.INMateriaId));

                        if (index == 0)
                            element.Add(new XElement("idOrigem", titCategory.TitCode), new XElement("nomeOrigem", titCategory.TitDescription), new XElement("idSiorg", titCategory.TitIdSiorg), new XElement("uf", null), new XElement("nomeMunicipio", null), new XElement("idMunicipioIbge", null)
);
                        else
                        {
                            var newElemnt = new XElement("origemPai", new XElement("idOrigem", titCategory.TitCode), new XElement("nomeOrigem", titCategory.TitDescription), new XElement("idSiorg", titCategory.TitIdSiorg), new XElement("uf", null), new XElement("nomeMunicipio", null), new XElement("idMunicipioIbge", null)
);
                            tempElem.Add(newElemnt);
                            tempElem = newElemnt;
                        }

                        index = index + 1;
                    }

                    return element;
                }
                catch (Exception ex)
                {
                    logger.LogError( ex, "error loading category tree");
                }

                logger.LogInformation("Returning Category from Category  Tree: ");

                var categoryTree = await categoryService.GetCategoryTreeAsync(article.INPortalCategoryId, article.INSortBy);
                logger.LogInformation(categoryTree.ToString());
                return categoryTree;
            }

            private List<TitCategory> GetTitCategories(string insortBy)
            {
                var titCategories = new List<TitCategory>();

                try
                {
                    var splittedInSortBy = insortBy.Split(':');

                    var rootInSortyBy = splittedInSortBy.FirstOrDefault();

                    if ((rootInSortyBy == null))
                        throw new Exception($"Wrong rootInSortyBy : {insortBy}");

                    var categoryRootSortBy = string.Format("{0:00000000}", Convert.ToInt32(rootInSortyBy));

                    var category = mainCategories.FirstOrDefault(a => a.INSortBy == categoryRootSortBy);

                    if ((category == null))
                        throw new Exception($"There is not Root Category With INsORTbY= {categoryRootSortBy}");

                    TitCategory mainCategory = CreateTitCategory(category);

                    titCategories.Add(mainCategory);

                    var titInsortBy = rootInSortyBy;

                    for (int index = 1; index <= splittedInSortBy.Count() - 1; index++)
                    {
                        if (Convert.ToInt32(splittedInSortBy[index]) == 0)
                            break;

                        titInsortBy = string.Concat(titInsortBy, ":", splittedInSortBy[index]);

                        var titCategoryByInSortBy = GetTitCategoryByInSortyBy(titInsortBy);

                        if (titCategoryByInSortBy != null)
                            titCategories.Add(titCategoryByInSortBy);
                    }
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, $"Houve um problema ao tentar pegar categorias do Artigo com INSortby: {insortBy}");
                    throw new Exception(string.Format("Houve um problema ao tentar pegar categorias do Artigo com INSortby: {0}", insortBy));
                }
                return titCategories;
            }
            private TitCategory GetTitCategoryByInSortyBy(string insortby)
            {
                logger.LogInformation($"GetTitCategoryByInSortyBy: {insortby}");

                var titArticle = this.titArticles.FirstOrDefault(a => a.INSortBy == insortby);

                if ((titArticle == null))
                {
                    logger.LogInformation($"Could not get Tit Article with INSortBy: {insortby}");
                    return null/* TODO Change to default(_) if this is not a reference type */;
                }

                return CreateTitCategory(titArticle);
            }

            private TitCategory CreateTitCategory(ArticleModel article)
            {
                var titCategory = new TitCategory();
                {
                    var withBlock = titCategory;
                    withBlock.IsRoot = false;
                    withBlock.TitCode = article.INTitCode;
                    withBlock.TitDescription = article.INTitDescription;
                    withBlock.TitIdSiorg = article.INTitIdSiorg;
                }

                logger.LogInformation($"Tit Article {article.INTitCode}, {article.INTitDescription}, {article.INTitIdSiorg} ");

                return titCategory;
            }
            private TitCategory CreateTitCategory(CategoryModel category)
            {
                var mainCategory = new TitCategory();
                {
                    var withBlock = mainCategory;
                    withBlock.IsRoot = true;
                    withBlock.TitCode = category.Code;
                    withBlock.TitDescription = category.Description;
                    withBlock.TitIdSiorg = category.IdSiorg;
                }

                return mainCategory;
            }


        }
    }
}
