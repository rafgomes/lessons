using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Leitor_ABCConfig.Entities
{
    [XmlRoot(ElementName = "TagsValidation")]
    public class TagsValidation
    {

        [XmlElement(ElementName = "RepFolderIds")]
        public object RepFolderIds { get; set; }

        [XmlElement(ElementName = "EditFolderIds")]
        public object EditFolderIds { get; set; }
    }
}
