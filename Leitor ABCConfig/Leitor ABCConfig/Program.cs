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
                        





            Console.ReadLine();
        }

    }
}
