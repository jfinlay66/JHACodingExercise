using JHACodingExercise.Domain;
using Microsoft.AspNetCore.Mvc;

namespace JHACodingExercise.Controllers
{
    /// <summary>
    /// Controller to allow access to the twitter statistics
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class TwitterStatisticsController : ControllerBase
    {
        private readonly TwitterStatistics _twitterStatistics;

        /// <summary>
        /// Public constructor
        /// </summary>
        /// <param name="twitterStatistics">DI TwitterStatistics Singleton</param>
        public TwitterStatisticsController(TwitterStatistics twitterStatistics)
        {
            _twitterStatistics = twitterStatistics;
        }

        /// <summary>
        /// Get endpoint
        /// </summary>
        /// <returns>TwitterStatistics</returns>
        [HttpGet]
        public TwitterStatistics Get()
        {

            return _twitterStatistics;
        }
    }
}
