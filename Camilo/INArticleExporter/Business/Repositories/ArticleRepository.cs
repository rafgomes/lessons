using Imprensa.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INPerformanceTest.Business.Repositories
{
    public class ArticleRepository : IArticleRepository
    {
        public async Task UpdateArticleAsync(ArticleModel article)
        {
            await Task.FromResult(0);
        }
    }
}
