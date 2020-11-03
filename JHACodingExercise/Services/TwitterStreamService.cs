using JHACodingExercise.Domain;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace JHACodingExercise.Services
{
    /// <summary>
    /// Class to run statistics gathering in the background
    /// </summary>
    public class TwitterStreamService : BackgroundService
    {
        private const string TWITTER_SAMPLED_STREAM_URL = "https://api.twitter.com/2/tweets/sample/stream";
        private readonly string _bearerToken; 

        private readonly ILogger<TwitterStreamService> _logger;
        private readonly TwitterStatistics _twitterStatistics;

        /// <summary>
        /// Public constructor
        /// </summary>
        /// <param name="logger">DI logger</param>
        /// <param name="twitterStatistics">DI statistics class</param>
        public TwitterStreamService(IConfiguration configuration, ILogger<TwitterStreamService> logger, TwitterStatistics twitterStatistics)
        {
            _logger = logger;
            _twitterStatistics = twitterStatistics;

            _bearerToken = configuration["TwitterApi:BearerToken"];
        }

        /// <summary>
        /// Background thread process
        /// </summary>
        /// <param name="stoppingToken">cancellation token</param>
        /// <returns>Task</returns>
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogDebug($"TwitterStreamService is starting.");

            stoppingToken.Register(() =>
                _logger.LogDebug($" TwitterStreamService background task is stopping."));

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogDebug($"TwitterStreamService task is calling Twitter Sampled Stream endpoint.");

                try
                {
                    var client = new HttpClient();
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_bearerToken}");

                    // add tweet.fields=entities to URI
                    var builder = new UriBuilder(TWITTER_SAMPLED_STREAM_URL);
                    var query = HttpUtility.ParseQueryString(builder.Query);
                    query["tweet.fields"] = "entities";
                    builder.Query = query.ToString();
                    string url = builder.ToString();

                    var stream = await client.GetStreamAsync(url);

                    // process stream from twitter
                    using (var reader = new StreamReader(stream))
                    {
                        while (!reader.EndOfStream)
                        {
                            string line = reader.ReadLine();

                            // process each line in a new thread - fire and forget, don't need return values
                            var task = new Task(() => _twitterStatistics.ProcessTweet(line));
                            task.Start();
                        }
                    }
                }
                catch (Exception e)
                {
                    _logger.LogError(e, $"TwitterStreamService encountered an exception - continuing until cancelled");
                }
            }

            _logger.LogDebug($"TwitterStreamService background task is stopping.");
        }
    }
}
