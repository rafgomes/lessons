using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Leitor_ABCConfig.Entities
{
    [XmlRoot(ElementName = "ABCConfig")]
    public class ABCConfig
    {
        //[XmlElement(ElementName = "PrintPage")]
        //public PrintPage PrintPage { get; set; }

        //[XmlElement(ElementName = "TagsValidation")]
        //public TagsValidation TagsValidation { get; set; }

        //[XmlElement(ElementName = "TimelineSegmentImage")]
        //public TimelineSegmentImage TimelineSegmentImage { get; set; }

        //[XmlElement(ElementName = "ImageDummyClasses")]
        //public ImageDummyClasses ImageDummyClasses { get; set; }

        [XmlArray("ImageDummyClasses")]
        [XmlArrayItem("ImageDummyClass")]
        public List<ImageDummyClass> ImageDummyClasses { get; set; }

        public List<string> ListaNomes { get; set; }
    }

}
