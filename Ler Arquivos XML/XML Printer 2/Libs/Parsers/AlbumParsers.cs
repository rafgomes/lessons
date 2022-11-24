using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml;
using XML_Printer.Entities;

namespace XML_Printer.Libs.Parsers
{
    public class AlbumParsers
    {
        public Album GetAlbum(string xmlPath)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlPath);

            string xml = xmlDoc.OuterXml;

            XmlSerializer serializer = new XmlSerializer(typeof(Album));
            using (StringReader reader = new StringReader(xml))
            {
                var album = (Album)serializer.Deserialize(reader);
                return album;
            }
            
        }
    }
}
