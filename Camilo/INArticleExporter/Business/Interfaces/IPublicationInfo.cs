using System;

namespace INPerformanceTest.Business.Interfaces
{
    public interface IPublicationInfo
    {
        DateTime PublicationDate { get; set; }
        string TitleName { get; set; }
        string WebEditionNumber { get; set; }
        bool isExtra { get; }
        bool IsSuplement { get; }
        string INEditionYear { get; set; }
        string INEditionNumber { get; set; }
    }
}
