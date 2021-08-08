namespace StreetWorkout.Test.Services
{
    using Xunit;
    using Mocks;

    using StreetWorkout.Data;
    using StreetWorkout.Data.Models;
    using StreetWorkout.Data.Models.Enums;
    using StreetWorkout.Services.Homes;
    using StreetWorkout.Services.Homes.Models;

    public class HomeServiceTest
    {
        public const string UserId = "Vs1";

        [Fact]
        public void IndexViewModelShouldReturnIndexServiceModelWithCorrectData()
        {
            // Arrange
            var data = GetDatabase();
            var mapper = MapperMock.Instance;

            var homeService = new HomeService(data, mapper);

            // Act
            var result = homeService.IndexViewModel(UserId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<IndexServiceModel>(result);
            Assert.True(result.IsAccountCompleted);
            Assert.True(result.IsTrainer);
        }

        private static StreetWorkoutDbContext GetDatabase()
        {
            var data = DatabaseMock.Instance;

            data.Users.Add(new ApplicationUser
            {
                Id = UserId,
                IsAccountCompleted = true,
                UserRole = UserRole.Trainer,
            });
            data.SaveChanges();

            return data;
        }
    }
}
