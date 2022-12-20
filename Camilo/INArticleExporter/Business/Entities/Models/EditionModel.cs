using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INPerformanceTest.Business.Entities.Models
{
    public class EditionModel
    {
        public Int32 Id { get; set; }
        public string Name { get; set; }
        public string INGroup { get; set; }
        public string webEditionNumber { get; set; }
        public string INEditionNumber { get; set; }
        public string INEditionYear { get; set; }
        public string Description { get; set; }
        public int TitleId { get; set; }
        public DateTime PublicationDate { get; set; }
        public int LeftMasterPageId { get; set; }
        public int RightMasterPageId { get; set; }
        public List<PageLayerModel> PageLayers { get; }
        public List<PageModel> UnassignedPages { get; }
        public List<MasterModel> MasterPages { get; }
        public bool LastEdition = false;
    }
}