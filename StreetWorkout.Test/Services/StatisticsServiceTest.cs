namespace StreetWorkout.Test.Services
{
    using System.Linq;
    using Xunit;

    using Data;
    using Data.Models;
    using Data.Models.Enums;
    using StreetWorkout.Services.Statistics;
    using StreetWorkout.Services.Statistics.Models;
    using Mocks;

    public class StatisticsServiceTest
    {
        [Fact]
        public void TotalShouldReturnCorrectStatisticsModelWithCorrectCountData()
        {
            // Arrange
            var data = GetDatabase();
            var statisticsService = new StatisticsService(data);

            // Act
            var result = statisticsService.Total();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<StatisticsModel>(result);
            Assert.Equal(1, result.TotalTrainers);
            Assert.Equal(4, result.TotalEnthusiasts);
            Assert.Equal(6, result.TotalWorkouts);
        }

        [Fact]
        public void TotalShouldReturnCorrectStatisticsModelWithInvalidCountData()
        {
            // Arrange
            var data = GetDatabase();
            var statisticsService = new StatisticsService(data);

            // Act
            var result = statisticsService.Total();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<StatisticsModel>(result);
            Assert.NotEqual(0, result.TotalTrainers);
            Assert.NotEqual(0, result.TotalEnthusiasts);
            Assert.NotEqual(1, result.TotalWorkouts);
        }

        public static StreetWorkoutDbContext GetDatabase()
        {
            var data = DatabaseMock.Instance;

            data.Users.Add(new ApplicationUser
            {
                UserRole = UserRole.Trainer,
            });

            data.Users.AddRange(Enumerable.Range(1, 4).Select(x => new ApplicationUser
            {
                UserRole = UserRole.Enthusiast,
            }));

            data.Workouts.AddRange(Enumerable.Range(1, 6).Select(x => new Workout()));

            data.SaveChanges();

            return data;
        }
    }
}
