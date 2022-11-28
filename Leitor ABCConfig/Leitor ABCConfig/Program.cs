using Leitor_ABCConfig.Entities;
using Leitor_ABCConfig.Libs;
using Leitor_ABCConfig.Libs.Parsers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.OleDb;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Leitor_ABCConfig
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Form1 form1 = new Form1();
            form1.ShowDialog();



            #region Consulta Antiga
            //ABCConfigParser parser = new ABCConfigParser();
            //ABCConfig abcConfig = parser.GetABCConfig(@"C:\Projets\abcparaguay\src\config\ABCConfig.xml");

            //Console.WriteLine("Write the Dummy Class:");
            //string pesquisa = Console.ReadLine();

            //var printer = new IDCPrinter();
            //printer.PrintIDC(abcConfig.ImageDummyClasses, pesquisa);

            //var creattxt = new TXTSaver();
            //creattxt.ToTXTFile(abcConfig.ImageDummyClasses, pesquisa);

            //Console.ReadLine();
            #endregion
        }

    }
}
