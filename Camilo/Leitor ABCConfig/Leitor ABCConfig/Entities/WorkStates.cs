using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Leitor_ABCConfig.Entities
{
    [XmlRoot(ElementName = "WorkStates")]
    public class WorkStates
    {

        [XmlElement(ElementName = "ProductionPrintedId")]
        public int ProductionPrintedId { get; set; }
    }
}
