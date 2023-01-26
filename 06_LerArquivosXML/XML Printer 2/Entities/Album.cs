using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace XML_Printer.Entities
{

    [XmlRoot(ElementName = "Album")]
    public class Album
    {

        [XmlElement(ElementName = "Genre")]
        public string Genre { get; set; }

        [XmlArray("Tracks")]  //<- Array Node element name
        [XmlArrayItem("Track")]  //<- Array item Element name
        public List<Track> Tracks { get; set; }

        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }

        [XmlAttribute(AttributeName = "description")]
        public string Description { get; set; }

        [XmlText]
        public string Text { get; set; }
    }

}
