namespace StreetWorkout.Test.Routing.Api
{
    using Xunit;
    using MyTested.AspNetCore.Mvc;
    using StreetWorkout.Controllers.Api;

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
