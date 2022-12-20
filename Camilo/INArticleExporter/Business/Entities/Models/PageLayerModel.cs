using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INPerformanceTest.Business.Entities.Models
{  
    public class PageLayerModel
    {
        public int Id { get; }
        public int Name { get; set; }
        public string LayerType { get; set; }
        public List<PageModel> Pages { get; }
        public List<LayerContentModel> Contents { get; }       
        public bool Spiked { get; set; }        
        public PageLayerModel(int pageLayerId)
        {
            this.Id = pageLayerId;
            this.Pages = new List<PageModel>();
            this.Contents = new List<LayerContentModel>();
        }

        public void MarkToDelete()
        {
            this.Spiked = true;
        }
        public void SetActive()
        {
            this.Spiked = false;
        }
    }
}
