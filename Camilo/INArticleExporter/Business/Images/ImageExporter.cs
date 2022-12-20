using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Imprensa.Business.Entities;
using Imprensa.Business.Interfaces;

namespace Imprensa
{
    namespace Business
    {
        public class ImageExporter : IImageExporter
        {
            private ArticleModel article;

            private IGN4ImageExporter gn4ImageExporter;

            public ImageExporter(IGN4ImageExporter gn4ImageExporter, IConfig config)
            {
                this.gn4ImageExporter = gn4ImageExporter;
            }
            public async Task Export(ArticleModel article)
            {
                List<Task> tasks = new List<Task>();
                foreach (var content in article.Contents)
                {
                    if ((content.ContentType.ToLower() == "photocaption"))
                    {
                        tasks.Add(gn4ImageExporter.ExportImg(content.MediaId, content.Name));                     
                    }
                }
                if(tasks.Count > 0)
                    await Task.WhenAll(tasks);
            }

            public async Task Export(List<ArticleModel> articleModels)
            {
                List<Task> tasks = new List<Task>();
                foreach (var article in articleModels)
                {
                    Task task = Export(article);
                    tasks.Add(task);

                }
                if (tasks.Count > 0)
                    await Task.WhenAll(tasks);
            }    

        }
    }
}
