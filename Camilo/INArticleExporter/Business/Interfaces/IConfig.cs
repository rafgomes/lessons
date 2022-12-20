
using System;

public interface IConfig
{  
    string PortalDestFolder { get; }
    string ConnectionString { get;  }
    string KafkaDODFMetadatosTopic { get; }
    string KafkaDOMetadatosTopic { get; }
    string KafkaDOArticleTopic { get; }
    string KafkaDODFArticleTopic { get; } 
    string BrokerList { get; }
    string ClientId { get; }
    string MessageTimeoutMs { get; }
    string KeyLocation { get; }
    string CertificateLocation { get; }
    string CaLocation { get; }
    string KeyPassword { get; }
    string MessageSizeBytes { get; }
}


