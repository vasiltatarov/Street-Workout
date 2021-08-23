namespace StreetWorkout.Controllers.Api
{
    using Microsoft.AspNetCore.Mvc;
    using StreetWorkout.Services.Statistics;
    using StreetWorkout.Services.Statistics.Models;

    [Route("api/statistics")]
    public class StatisticsApiController : ApiController
    {
        private readonly IStatisticsService statistics;

        public StatisticsApiController(IStatisticsService statistics)
            => this.statistics = statistics;

        public StatisticsModel GetStatistics()
            => this.statistics.Total();
    }
}
