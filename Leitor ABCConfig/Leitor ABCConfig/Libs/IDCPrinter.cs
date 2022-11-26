using Leitor_ABCConfig.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.IO;

namespace Leitor_ABCConfig.Libs
{
    public class IDCPrinter
    {
        #region PrintIDC1
        //public void PrintIDC(List<ImageDummyClass> idclasses)
        //{
           
        //    foreach (ImageDummyClass imageDummyClass in idclasses)
        //    {
        //        Console.WriteLine($"Name: {imageDummyClass.Name} // Width = {imageDummyClass.Width} // Height = {imageDummyClass.Height} // X = {imageDummyClass.X} // Y = {imageDummyClass.Y}");
        //    }
        //    Console.WriteLine("===================================");

        //}
        #endregion

        #region Filtro
        public void PrintIDC(List<ImageDummyClass> lista, string name) //sobrecarga de metodos https://balta.io/blog/orientacao-a-objetos-sobrescrita-sobrecarga
        {
            var imageDummy = (from item in lista
                              where item.Name == name
                              select item).FirstOrDefault(); //se não existir retorna null 
            if (imageDummy != null)
            {
                Console.WriteLine($"Name: {imageDummy.Name} X: {imageDummy.X} Y: {imageDummy.Y}");
            }
            else
            {
                Console.WriteLine($"Nome {name} não encontrado!");
            }
        }
        #endregion
    }
}
