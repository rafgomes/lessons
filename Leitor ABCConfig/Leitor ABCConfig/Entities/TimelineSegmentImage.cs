using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Leitor_ABCConfig.Entities
{
    [XmlRoot(ElementName = "TimelineSegmentImage")]
    public class TimelineSegmentImage
    {

        [XmlElement(ElementName = "Width")]
        public object Width { get; set; }

        [XmlElement(ElementName = "Height")]
        public object Height { get; set; }

        [XmlElement(ElementName = "FolderId")]
        public object FolderId { get; set; }
    }
}
