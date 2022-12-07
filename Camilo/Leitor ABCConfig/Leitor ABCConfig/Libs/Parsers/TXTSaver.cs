using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Leitor_ABCConfig.Entities;
using System.Windows.Forms;

namespace Leitor_ABCConfig.Libs.Parsers
{
    public class TXTSaver
    {
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
                        MessageBox.Show("Arquivo TXT Gerado!");
                    }
                    else
                    {
                        MessageBox.Show("Arquivo não foi gerado!");
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
