using System.Net;
using Monitor.Helpers;

namespace Monitor;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly Sites? _sites;

    public Worker(ILogger<Worker> logger, IConfiguration _conf)
    {
        _logger = logger;
        _sites = _conf.GetSection("Sites").Get<Sites>();
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            HttpStatusCode status = await Requesters.GetStatusFromUrl(_sites.Url);
            if(status != HttpStatusCode.OK)
            {
                string nameFile = string.Format("logFile_{0}.txt", DateTime.Now.ToString("yyyyMMdd"));
                string path = Path.Combine(@"C:\Temp", nameFile);
                StreamWriter logFile = new StreamWriter(path, true);
                string message = string.Format("O site {0} ficou fora do ar no dia {1}", _sites.Url, DateTime.Now.ToString());
                logFile.WriteLine(message);
                logFile.Close();
                _logger.LogInformation(message);
            }

            
            await Task.Delay(1000, stoppingToken);
        }
    }
}
