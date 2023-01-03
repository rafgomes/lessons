namespace MeuRobot
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        int tempoExcecucao;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
            tempoExcecucao = 3000;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                string newText = "Teste realizado as: " + DateTimeOffset.Now.TimeOfDay;

                string oldText = File.ReadAllText(@"C:\Temp\Teste.txt");

                using (StreamWriter myFile = new StreamWriter(@"C:\Temp\Teste.txt"))
                {
                    myFile.WriteLine(oldText + newText + Environment.NewLine);
                }

                await Task.Delay(tempoExcecucao, stoppingToken);
            }
        }
    }
}