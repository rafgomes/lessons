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
using XML_Printer.Entities;
using XML_Printer.Libs;
using XML_Printer.Libs.Parsers;

namespace XML_Printer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            AlbumParsers parser = new AlbumParsers();
            Album album = parser.GetAlbum(@"C:\Projets\Lessons\Ler Arquivos XML\Album.xml");

            var printer = new AlbumPrinter();
            printer.PrintAlbum(album);
            
            //var tprinter = new tracksPrinter();
            //tprinter.PrintTracks(album);
            
            
            
            
            Console.ReadLine();


        }
    }
}
