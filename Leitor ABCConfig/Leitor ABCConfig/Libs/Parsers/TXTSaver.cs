using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Leitor_ABCConfig.Entities;

namespace Leitor_ABCConfig.Libs.Parsers
{
    public class TXTSaver
    {
        public void ToTXTFile(List<ImageDummyClass> idclasses)
        {
            StreamWriter sw = new StreamWriter(@"C:\temp\Resultado.txt", true, Encoding.UTF8);

            foreach (ImageDummyClass imageDummyClass in idclasses)
            {
                sw.WriteLine($"Name: {imageDummyClass.Name} // Width = {imageDummyClass.Width} // Height = {imageDummyClass.Height} // X = {imageDummyClass.X} // Y = {imageDummyClass.Y}");
            }
            sw.WriteLine("");
            sw.Close();

            Console.WriteLine("Resultado.txt Gerado!!!");

        }
    }
}
