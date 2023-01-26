using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace XML_Printer.Entities
{


    [XmlRoot(ElementName = "Track")]
    public class Track
    {
        [XmlElement(ElementName = "Path")]
        public string Path { get; set; }

        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }

        [XmlText]
        public string Text { get; set; }
    }
    
}
