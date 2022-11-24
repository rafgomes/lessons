using Leitor_ABCConfig.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leitor_ABCConfig.Libs
{
    public class IDCPrinter
    {
        public void PrintIDC(List<ImageDummyClass> idclasses)
        {
            foreach (ImageDummyClass imageDummyClass in idclasses)
            {
                Console.WriteLine($"Name: {imageDummyClass.Name} // Width = {imageDummyClass.Width} // Height = {imageDummyClass.Height} // X = {imageDummyClass.X} // Y = {imageDummyClass.Y}");
            }
        }


    }
}
