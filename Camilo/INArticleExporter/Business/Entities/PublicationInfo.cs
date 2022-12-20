using INPerformanceTest.Business.Interfaces;
using System;
using System.Dynamic;

namespace INPerformanceTest.Business.Services
{
    internal class PublicationInfo : IPublicationInfo
    {
        private PublicationInfo(DateTime publicationDate, string titleName, string webEditionNumber, string inEditionYear, string inEditionNumber)
        {
            PublicationDate = publicationDate;
            TitleName = titleName;
            WebEditionNumber = WebEditionNumber;
            INEditionYear = inEditionYear;
            INEditionNumber = inEditionNumber;
        }

        public static PublicationInfo CreateInstance(DateTime publicationDate, string titleName, string WebEditionNumber, string inEditionYear, string inEditionNumber)
        {
            return new PublicationInfo(publicationDate, titleName, WebEditionNumber, inEditionYear, inEditionNumber);
        }

        public DateTime PublicationDate { get; set; }
        public string TitleName { get ; set; }
        public string WebEditionNumber { get; set; }
        public bool isExtra => TitleName.Contains("E");
        public bool IsSuplement => TitleName.Contains("DO1A") || TitleName.Contains("SUP");
        public string INEditionYear { get; set; }
        public string INEditionNumber { get; set; }
    }
}