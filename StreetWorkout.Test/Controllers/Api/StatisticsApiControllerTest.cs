namespace StreetWorkout.Test.Controllers.Api
{
    using System.Linq;
    using MyTested.AspNetCore.Mvc;
    using Shouldly;
    using StreetWorkout.Controllers.Api;
    using StreetWorkout.Data.Models;
    using StreetWorkout.Services.Statistics.Models;
    using StreetWorkout.Test.Mocks.Services;
    using Xunit;

    public class StatisticsApiControllerTest
    {
        [Fact]
        public void GetStatisticsShouldReturnTotalStatistics()
        {
            // Arrange
            var statisticsService = StatisticsServiceMock.Instance;
            var statisticController = new StatisticsApiController(statisticsService);

            // Act
            var result = statisticController.GetStatistics();

            // Assert
            Assert.IsType<StatisticsModel>(result);
            Assert.NotNull(result);
            Assert.Equal(5, result.TotalTrainers);
            Assert.Equal(10, result.TotalEnthusiasts);
            Assert.Equal(20, result.TotalWorkouts);
        }

        [Fact]
        public void GetStatisticsShouldReturnCorrectTotalStatistics()
            => MyController<StatisticsApiController>
                .Instance()
                .WithUser()
                .WithData(data => data
                    .WithEntities(
                        Enumerable.Range(1, 5)
                        .Select(x => new Workout())))
                .Calling(c => c.GetStatistics())
                .ShouldReturn()
                .ResultOfType<StatisticsModel>(result => result
                    .Passing(data =>
                {
                    data.TotalWorkouts.ShouldBe(5);
                }));
    }
}
