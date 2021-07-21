﻿namespace StreetWorkout.Controllers.Api
{
    using Microsoft.AspNetCore.Mvc;
    using Services.Statistics;

    [ApiController]
    [Route("api/statistics")]
    public class StatisticsApiController : ControllerBase
    {
        private readonly IStatisticsService statistics;

        public StatisticsApiController(IStatisticsService statistics)
            => this.statistics = statistics;

        public StatisticsModel GetStatistics()
            => this.statistics.Total();
    }
}
