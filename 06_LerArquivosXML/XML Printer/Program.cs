using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;


namespace XML_Printer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //XmlDocument xmlDoc = new XmlDocument();
            //xmlDoc.Load(@"C:\Projets\Lessons\Ler Arquivos XML\ABCConfig.xml");

            //XmlNode vHeith = xmlDoc.DocumentElement.SelectSingleNode("/ABCConfig/ImageDummyClass/Height");
            //XmlNode vWidth = xmlDoc.DocumentElement.SelectSingleNode("/ABCConfig/ImageDummyClass/Width");
            //XmlNode vX = xmlDoc.DocumentElement.SelectSingleNode("/ABCConfig/ImageDummyClass/X");
            //XmlNode vY = xmlDoc.DocumentElement.SelectSingleNode("/ABCConfig/ImageDummyClass/Y");

            //string H = vHeith.InnerText;
            //string W = vWidth.InnerText;
            //string X = vX.InnerText;
            //string Y = vY.InnerText;

            //Console.WriteLine($"Height = {H} / Width = {W} / X = {X} / Y = {Y}");

            //Console.ReadLine();

            //=================================================================================================

            XmlDocument xmlAlbum = new XmlDocument();
            xmlAlbum.Load(@"C:\Projets\Lessons\Ler Arquivos XML\Album.xml");

            XmlNodeList lista;
            XmlElement root = xmlAlbum.DocumentElement;
            lista = root.SelectNodes("/Album/Tracks/Track");
            foreach (XmlNode faixa in lista)
            {
                string trackName = faixa.Attributes["name"].Value;
                string path = faixa.SelectSingleNode("Path").InnerText;

                Console.WriteLine($"Faixa: {trackName} em {path}");

            }
            Console.ReadLine();




        }


    }
}
