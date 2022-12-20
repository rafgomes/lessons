using INPerformanceTest.Business.Interfaces;
using System;
public class IMPNAConfig : IConfig
{
    public string PortalDestFolder { get; set; }
    public string KafkaDODFMetadatosTopic { get; set; }
    public string KafkaDOMetadatosTopic { get; set; }
    public string KafkaDOArticleTopic { get; set; }
    public string KafkaDODFArticleTopic { get; set; }
    public string BrokerList { get; set; }
    public string ClientId { get; set; }
    public string MessageTimeoutMs { get; set; }
    public string KeyLocation { get; set; }
    public string CertificateLocation { get; set; }
    public string CaLocation { get; set; }
    public string KeyPassword { get; set; }
    public string MessageSizeBytes { get; set; }
    public string ConnectionString { get; set; }


}
