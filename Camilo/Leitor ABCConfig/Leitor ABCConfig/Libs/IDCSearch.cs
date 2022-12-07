using Leitor_ABCConfig.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.IO;
using System.Windows.Forms;

namespace Leitor_ABCConfig.Libs
{
    public class IDCSearch
    {
        public ImageDummyClass GetImageDummyClass(List<ImageDummyClass> lista, string name) //sobrecarga de metodos https://balta.io/blog/orientacao-a-objetos-sobrescrita-sobrecarga
        {

            var imageDummy = (from item in lista
                              where item.Name == name
                              select item).FirstOrDefault(); //se não existir retorna null 

            return imageDummy;
        }

    }
}
