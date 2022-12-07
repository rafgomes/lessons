using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Leitor_ABCConfig.Entities
{
    [XmlRoot(ElementName = "PrintPage")]
    public class PrintPage
    {

        [XmlElement(ElementName = "ExcPrinters")]
        public double ExcPrinters { get; set; }

        [XmlElement(ElementName = "ControlPrinters")]
        public string ControlPrinters { get; set; }

        [XmlElement(ElementName = "WorkStates")]
        public WorkStates WorkStates { get; set; }
    }
}
