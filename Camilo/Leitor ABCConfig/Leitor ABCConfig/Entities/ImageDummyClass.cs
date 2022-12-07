using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Leitor_ABCConfig.Entities
{
    [XmlRoot(ElementName = "ImageDummyClass")]
    public class ImageDummyClass
    {
        [XmlElement(ElementName = "Width")]
        public string Width { get; set; }

        [XmlElement(ElementName = "Height")]
        public string Height { get; set; }

        [XmlElement(ElementName = "X")]
        public string X { get; set; }

        [XmlElement(ElementName = "Y")]
        public string Y { get; set; }

        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }

        [XmlText]
        public string Text { get; set; }
    }


}
