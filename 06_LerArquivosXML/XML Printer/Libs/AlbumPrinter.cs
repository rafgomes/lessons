using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XML_Printer.Entities;

namespace XML_Printer.Libs
{
    public class AlbumPrinter
    {
        public void PrintAlbum(Album album)
        {
            Console.WriteLine(album.Name);
        }
    }
}
