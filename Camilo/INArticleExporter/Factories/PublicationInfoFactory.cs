using INPerformanceTest.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INPerformanceTest.Factories
{
    public class PublicationInfoFactory : IPublicationInfoFactory
    {
        private readonly IEditionService editionService;

        public PublicationInfoFactory(IEditionService editionService)
        {
            this.editionService = editionService;
        }

        public Task<IPublicationInfo> GetPublicationInfo(int ediionId)
        {
            return editionService.GetPublicationInfoByEditionId(ediionId);
        }
    }
}
