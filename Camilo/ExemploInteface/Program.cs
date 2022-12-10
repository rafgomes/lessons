using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTeste
{

    public interface IGetArticle
    {
        string GetArtigo(string id);
    }

    public class DbRepository : IGetArticle
    {
        public string GetArtigo(string id)
        {
            //faz a consulta no banco sql 
            // cria um arquivo xml com os dados do banco
            //convert para string
            Console.WriteLine($"Fazendo request no SQL para o artigo:{id}");

            string xmlstring = $@"<document id=""{id}""><Text>Hello World</Text></document>";

            return xmlstring;
        }
    }

    public class ApiRequests : IGetArticle
    {
        public string GetArtigo(string id)
        {
            //consulta o api  
            //convert xdocument para string
            Console.WriteLine($"Fazendo request no API para o artigo:{id}");
            string xmlstring = $@"<document id=""{id}""><Text>Hello World</Text></document>";
            return xmlstring;
        }
    }


    public class ExportaArtigo
    {
        private readonly IGetArticle getArticle;

        public ExportaArtigo(IGetArticle getArticle)
        {
            this.getArticle = getArticle;
        }
        public void ExportXml(string id)
        {

            string resultXml = getArticle.GetArtigo(id);

            File.WriteAllText($@"c:\temp\{id}.xml", resultXml);
        }
    }


    public static class GetArticleFactory
    {
        public static IGetArticle CreateGetArticle(string tipo)
        {
            if (tipo == "DB")
            {
                IGetArticle getArticle = new DbRepository();
                return getArticle;
            }

            if (tipo == "API")
            {
                IGetArticle getArticle = new ApiRequests();
                return getArticle;
            }

            return new DbRepository();
        }

    }

    public class Program
    {

        public static void Main(string[] args)
        {
            string idMateria = args[0];
            string tipo = "DB";
            if (args.Length > 1)
            {
                tipo = args[1];
            }
            var dependencia = GetArticleFactory.CreateGetArticle(tipo);

            var exportARtigo = new ExportaArtigo(dependencia);
            exportARtigo.ExportXml(idMateria);

            Console.ReadKey();
        }
    }
}
