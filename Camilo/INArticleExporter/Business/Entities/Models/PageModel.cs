using Imprensa.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INPerformanceTest.Business.Entities.Models
{
    public class PageModel 
    {
        public int Id { get; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public int Number { get; set; }
        public int MasterRefId { get; set; }
        public List<PagePreviewModel> Previews { get; set; }      
        public PageModel(int pageId)
        {
            this.Id = pageId;
            this.Previews = new List<PagePreviewModel>();        
          
        }
    }
}
