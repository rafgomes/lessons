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
        #region Old
        //    try
        //    {
        //        using (StreamWriter sw = new StreamWriter(@"C:\temp\Resultado.txt", true, Encoding.UTF8))
        //        {
        //            foreach (ImageDummyClass imageDummyClass in idclasses)
        //            {
        //                sw.WriteLine($"Name: {imageDummyClass.Name} // Width = {imageDummyClass.Width} // Height = {imageDummyClass.Height} // X = {imageDummyClass.X} // Y = {imageDummyClass.Y}");
        //            }
        //            sw.WriteLine("");
        //        }

        //        //Console.WriteLine("Resultado.txt Gerado!!!");

        //    }
        //    catch(Exception ex)
        //    {
        //        Console.WriteLine(ex.ToString());
        //    }
        //}
        #endregion

        public void ToTXTFile(List<ImageDummyClass> lista, string name)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(@"C:\temp\Resultado.txt", true, Encoding.UTF8))
                {
                    var imageDummy = (from item in lista
                                      where item.Name == name
                                      select item).FirstOrDefault();

                    if (imageDummy != null)
                    {
                        sw.WriteLine($"Name: {imageDummy.Name} // Width = {imageDummy.Width} // Height = {imageDummy.Height} // X = {imageDummy.X} // Y = {imageDummy.Y}");
                        sw.WriteLine(" ");
                        Console.WriteLine("Arquivo TXT Gerado!");
                    }
                    else
                    {
                        Console.WriteLine("Arquivo não foi gerado!");
                    }

                }

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }
    }
}
