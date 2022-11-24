using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Leitor_ABCConfig.Entities
{
    [XmlRoot(ElementName = "ImageDummyClasses")]
    public class ImageDummyClasses
    {

        [XmlElement(ElementName = "ImageDummyClass")]
        public List<ImageDummyClass> ImageDummyClass { get; set; }
    }
}
