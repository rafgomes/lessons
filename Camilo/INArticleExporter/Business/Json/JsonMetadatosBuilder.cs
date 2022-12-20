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
using System.Runtime.Serialization.Json;
using System.Text.RegularExpressions;
using Imprensa.Business.Entities;
using INPerformanceTest.Business.Entities.Json;
using INPerformanceTest.Business.Interfaces;

namespace Imprensa
{
    namespace Business
    {
        public class JsonMetadatosBuilder
        {
            private readonly IPublicationInfo publicationInfo;

            public JsonMetadatosBuilder(IPublicationInfo publicationInfo)
            {
                this.publicationInfo = publicationInfo;
            }
            public JsonJornal GetJsonJornal(List<PublishArticle> publishArticles)
            {               

                var json = new JsonJornal();
                json.publicacao = new Publication();

                json.publicacao.jornal = new Jornal();

                if ((publicationInfo.TitleName.Contains("DODF")))
                    json.publicacao.jornal.idSecao = 0;
                else
                    json.publicacao.jornal.idSecao = Convert.ToInt32(Regex.Match(publicationInfo.TitleName, @"\d+").Value);

                // Dim idJornal = GSIUtils.GetIdJornal(editionProperties.TitleName, editionModel.INEditionNumber, editionModel.Name)
                var idJornal = publicationInfo.WebEditionNumber;
                json.publicacao.jornal.isExtra = publicationInfo.isExtra;
                json.publicacao.jornal.isSuplemento = publicationInfo.IsSuplement;
                json.publicacao.jornal.idJornal = Convert.ToInt32(idJornal);
                json.publicacao.jornal.numeroEdicao = publicationInfo.INEditionNumber;
                json.publicacao.jornal.ano = publicationInfo.INEditionYear;
                json.publicacao.dataPublicacao = publicationInfo.PublicationDate.ToString("yyyy-MM-dd");

                List<string> articles = (from a in publishArticles
                                      select a.Article.INMateriaId).ToList();
                var childrenArticles = (from a in publishArticles
                                        where a.Article.HasParent
                                        select a.Article.INMateriaId).ToList();

                json.publicacao.idMaterias = articles.Select(Int32.Parse).ToList();

                json.publicacao.quantidadeMaterias = Convert.ToInt32(json.publicacao.idMaterias.Count);
                if ((publicationInfo.TitleName.Contains("DO1A")))
                    json.publicacao.quantidadeMateriasFilhas = 0;
                else
                    json.publicacao.quantidadeMateriasFilhas = childrenArticles.Count;
                return json;
            }
        }
    }
}
