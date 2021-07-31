namespace StreetWorkout.Test.Controllers.Api
{
    using Xunit;
    using StreetWorkout.Controllers.Api;
    using StreetWorkout.Services.Statistics.Models;
    using StreetWorkout.Test.Mocks.Services;

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
    }
}
