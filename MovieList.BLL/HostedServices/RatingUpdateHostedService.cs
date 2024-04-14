using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MovieList.BLL.Interfaces;

namespace MovieList.BLL.HostedServices
{
    public class RatingUpdateHostedService : IHostedService, IDisposable
    {
        private Timer? _timer = null;
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<RatingUpdateHostedService> _logger;

        public RatingUpdateHostedService(IServiceProvider serviceProvider, ILogger<RatingUpdateHostedService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            var currentTime = DateTime.Now;

            var timeUntil3AM = DateTime.Today.AddDays(1).AddHours(3) - currentTime;

            if (timeUntil3AM.TotalMilliseconds < 0)
            {
                timeUntil3AM = timeUntil3AM.Add(TimeSpan.FromDays(1));
            }

            _timer = new Timer(UpdateMoviesRatingAsync, null, timeUntil3AM, TimeSpan.FromDays(1));

            return Task.CompletedTask;
        }

        private void UpdateMoviesRatingAsync(object state)
        {
            _logger.LogInformation("Started updating movie ratings");

            using (var scope = _serviceProvider.CreateScope())
            {
                var ratingService = scope.ServiceProvider.GetRequiredService<IRatingService>();
                ratingService.UpdateMoviesRatingAsync();
            }

            _logger.LogInformation("Movie ratings successfully updated");
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
