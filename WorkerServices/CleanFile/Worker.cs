using CleanFile.DTO;

namespace CleanFile
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IConfiguration _configuration;
        static Timer TmrCall;
        private Settings _settings;

        public Worker(ILogger<Worker> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);

                try
                {
                    _settings = new Settings();
                    _configuration.GetSection("Config").Bind(_settings);

                    if(_settings.LoopTime > 0)
                    {
                        TmrCall = new Timer(CleanConfig, null, 0, _settings.LoopTime * 60 * 1000);
                    }
                }
                catch
                {

                }
            }
        }

        private void CleanConfig(object? state)
        {
            var path = _settings.LocalPath;
            DirectoryInfo directory = new DirectoryInfo(path);
            FileInfo[] arq = directory.GetFiles("*.*");

            foreach (FileInfo fileinfo in arq)
            {
                if (fileinfo.FullName.Contains("Teste"))
                {
                    if (fileinfo.LastWriteTime < DateTime.Now.AddDays(0))
                    {
                        fileinfo.Delete();
                    }
                }
            }
        }
    }
}