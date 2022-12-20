using PostSharp.Aspects.Advices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace INPerformanceTest.Business.Entities.Models
{

    public class LayerContentModel
    {
        public LayerContentModel(int id) {
            TxtId = id;
        }
        public string Name { get; set; }
        public string ContentType { get; set; }
        public int MediaId { get; set; }
        public int TxtId { get; set; }
        public int FX { get; set; }
        public int FY { get; set; }
        public int FW { get; set; }

    }

}
