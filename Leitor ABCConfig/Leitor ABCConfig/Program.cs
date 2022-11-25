using Leitor_ABCConfig.Entities;
using Leitor_ABCConfig.Libs;
using Leitor_ABCConfig.Libs.Parsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Leitor_ABCConfig
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ABCConfigParser parser = new ABCConfigParser();
            ABCConfig classes = parser.GetABCConfig(@"C:\Projets\abcparaguay\src\config\ABCConfig.xml");

            var printer = new IDCPrinter();
            printer.PrintIDC(classes.ImageDummyClass);

            var creattxt = new TXTSaver();
            creattxt.ToTXTFile(classes.ImageDummyClass);

            //criar um novo método na class IDCPrinter que chama PrintIDC( listaDeDummyClassees, name); 
            //para imprimir apenas o Dummy que contem o valor do nome passado por parâmetro igual ao exemplo abaixo


            //var imageDummy = (from item in classes.ImageDummyClass
            //                  where item.Name == "ESC"
            //                  select item).FirstOrDefault(); //se não existir retorna null 
            //if(imageDummy != null)
            //{
            //    Console.WriteLine($"Name: {imageDummy.Name} X: {imageDummy.X} Y: {imageDummy.Y}")
            //} 


            
            Console.ReadLine();
        }

    }
}
