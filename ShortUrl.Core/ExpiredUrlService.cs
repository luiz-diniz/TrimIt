using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ShortUrl.Repository.Interfaces;

namespace ShortUrl.Core
{
    public class ExpiredUrlService : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ILogger<ExpiredUrlService> _logger;
        private readonly TimeSpan _interval = TimeSpan.FromSeconds(60);

        public ExpiredUrlService(IServiceScopeFactory serviceScopeFactory, ILogger<ExpiredUrlService> logger)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                DeleteExpiredUrls();
                await Task.Delay(_interval, stoppingToken);
            }
        }

        private void DeleteExpiredUrls()
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var urlRepository = scope.ServiceProvider.GetRequiredService<IUrlRepository>();

            var expiredUrlsCount = urlRepository.DeleteExpiredUrls();

            if (expiredUrlsCount > 0)
                _logger.LogInformation("{expiredUrlsCount} expired URL(s) deleted.", expiredUrlsCount);
        }
    }
}