using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using INPerformanceTest.Business.Entities.Models;
using Microsoft.VisualBasic;

namespace Imprensa
{
    namespace Business
    {
        namespace Entities
        {
            public class PageArticle
            {       
                public EditionModel EditionModel { get; set; }  

                public ArticleModel Article { get; set; }  

                public int PageNum { get; set; }
            }
        }
    }
}
