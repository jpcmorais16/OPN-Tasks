using OPN.Domain;

namespace Worker;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IConfiguration _configuration;

    public Worker(ILogger<Worker> logger, IUnitOfWork unitOfWork, IConfiguration configuration)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _configuration = configuration;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

            await UpdateSpreadsheet();

            await UpdateDatabase();

            await Task.Delay(1000, stoppingToken);
        }
    }

    private async Task UpdateSpreadsheet()
    {
        // get list of parameters of some type and put it on the spreadsheet for each repository/spreadsheet
    }

    private async Task UpdateDatabase()
    {
        
    }
}