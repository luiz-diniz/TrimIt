using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ShortUrl.Repository.Interfaces;

namespace ShortUrl.Core
{
    public class ExpiredUrlService : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly TimeSpan _interval = TimeSpan.FromSeconds(60);

        public ExpiredUrlService(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while(!stoppingToken.IsCancellationRequested)
            {
                await DeleteExpiredUrls();
                await Task.Delay(_interval, stoppingToken);
            }
        }

        private async Task DeleteExpiredUrls()
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var urlRepository = scope.ServiceProvider.GetRequiredService<IUrlRepository>();

            urlRepository.DeleteExpiredUrls();
        }
    }
}
