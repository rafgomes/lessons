using INPerformanceTest.Business.Entities.Models;
using INPerformanceTest.Business.Interfaces;
using INPerformanceTest.Entity;
using INPerformanceTest.Extensions;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INPerformanceTest.Business.Services
{
    public class EditionService : IEditionService
    {
        private readonly IEditionRepository editionRepository;

        public EditionService(IEditionRepository editionRepository)
        {
            ExpressionExtensions.CheckForNullArgument(() => editionRepository);

            this.editionRepository = editionRepository;
        }
        public async Task<IPublicationInfo> GetPublicationInfoByEditionId(int editionId)
        {
            var publicationInfo = await editionRepository.GetPublicationInfoByEditionId(editionId);

            if (publicationInfo == null)
                throw new Exception($"Could not load publication infor for Edition Id: {editionId}");

            return publicationInfo;
        }
        public async Task<List<EditionModel>> GetGroupEditionsById(int editionId)
        {
            List<EditionModel> editions = await editionRepository.GetGroupEditionsById(editionId);

            if (editions == null || editions.Count == 0)
                throw new Exception($"Could not load any editions for Id: {editionId}");

            return editions;
        }
    }
}
