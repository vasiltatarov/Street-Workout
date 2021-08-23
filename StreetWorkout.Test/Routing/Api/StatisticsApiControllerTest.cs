namespace StreetWorkout.Test.Routing.Api
{
    using MyTested.AspNetCore.Mvc;
    using StreetWorkout.Controllers.Api;
    using Xunit;

    public class StatisticsApiControllerTest
    {
        [Fact]
        public void GetStatisticsShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/api/statistics")
                .To<StatisticsApiController>(c => c.GetStatistics());
    }
}
