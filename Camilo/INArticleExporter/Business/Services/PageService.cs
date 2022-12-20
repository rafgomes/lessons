using Imprensa.Business;
using Imprensa.Business.Entities;
using INPerformanceTest.Business.Entities.Models;
using INPerformanceTest.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace INPerformanceTest.Business.Services
{



    public class PageDbService : IPageService
    {
        public Task<List<PageModel>> GetPagesByEditionAsync(int editionId)
        {
            throw new NotImplementedException();
        }
    }

    public class FactoryPageService
    {
        private readonly IApiService apiService;
        private readonly ICredentials credentials;
        private readonly ILogger logger;
        public FactoryPageService(IApiService apiService, ICredentials credentials, ILogger logger)
        {
            this.apiService = apiService;
            this.credentials = credentials;
            this.logger = logger;
        }

        public IPageService CreatePageService(string type)
        {

            switch (type)
            {
                case "API":
                    return new PageService(apiService, credentials, logger);
                case "DB":
                    return new PageDbService();
            }

            return new PageDbService();
        } 
    }


    public class PageService : IPageService
    {
        private readonly IApiService apiService;
        private readonly ICredentials credentials;
        private readonly ILogger logger;
        public PageService(IApiService apiService, ICredentials credentials, ILogger logger)
        {
            this.apiService = apiService;
            this.credentials = credentials;
            this.logger = logger;
        }
        public async Task<List<PageModel>> GetPagesByEditionAsync(int editionId)
        {
            var pages = new List<PageModel>();

            try
            {
                var strDoUrl = string.Format("{0}/do.ashx?ticket={1}&cmd=search&name=gn4:page[gn4:editionRef/@idref='{2}']&feed={3}", credentials.Url, credentials.Ticket, $"obj{editionId}", "GSIPageModel");
                XDocument xDoc = await apiService.LoadXDocumentAsync(strDoUrl);
                if (xDoc != null)
                {
                    XNamespace gn4 = "urn:schemas-teradp-com:gn4tera";
                    var xPages = xDoc.Descendants("page");
                    foreach (XElement xPage in xPages)
                    {
                        var page = new PageModel(Int32.Parse(xPage.Attribute("id").Value));
                        {
                            var withBlock = page;
                            withBlock.Name = xPage.Attribute("name").Value;
                            withBlock.ShortName = xPage.Attribute("shortName").Value;
                            withBlock.Number = Int32.Parse(xPage.Attribute("number").Value);
                            withBlock.MasterRefId = Int32.Parse(xPage.Attribute("masterRef").Value);
                        }
                        var xPagePreviews = xPage.Descendants("pagePreview");
                        foreach (XElement xPagePreview in xPagePreviews)
                        {
                            var pagePreview = new PagePreviewModel(Int32.Parse(xPagePreview.Attribute("id").Value));
                            page.Previews.Add(pagePreview);
                        }

                        pages.Add(page);
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error while fetching pages api");
            }

            return pages;
        }
    }
}
