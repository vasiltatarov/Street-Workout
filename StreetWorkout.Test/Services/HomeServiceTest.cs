namespace StreetWorkout.Test.Services
{
    using System.Linq;
    using System.Threading.Tasks;
    using StreetWorkout.Data.Models;
    using StreetWorkout.Data.Models.Enums;
    using StreetWorkout.Services.Homes;
    using StreetWorkout.Services.Homes.Models;
    using StreetWorkout.Test.Mocks;
    using Xunit;

    public class HomeServiceTest
    {
        public const string UserId = "Vs1";

        [Fact]
        public async Task IndexViewModelShouldReturnIndexServiceModelWithCorrectData()
        {
            // Arrange
            var data = DatabaseMock.Instance;

            await data.Users.AddAsync(new ApplicationUser
            {
                Id = UserId,
                IsAccountCompleted = true,
                UserRole = UserRole.Trainer,
            });
            await data.SaveChangesAsync();

            var mapper = MapperMock.Instance;

            var homeService = new HomeService(data, mapper);

            // Act
            var result = await homeService.IndexViewModel(UserId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<IndexServiceModel>(result);
            Assert.True(result.IsAccountCompleted);
            Assert.True(result.IsTrainer);
        }

        [Fact]
        public async Task WorkoutsShouldReturnCorrectCollectionWithThreeLatestWorkouts()
        {
            // Arrange
            var data = DatabaseMock.Instance;
            await data.Workouts.AddRangeAsync(Enumerable.Range(1, 5).Select(x => new Workout
            {
                Sport = new Sport(),
                BodyPart = new BodyPart(),
                User = new ApplicationUser(),
            }));
            await data.SaveChangesAsync();

            var mapper = MapperMock.Instance;
            var homeService = new HomeService(data, mapper);

            // Act
            var result = await homeService.Workouts();

            // Assert
            Assert.Equal(3, result.Count());
            Assert.Equal(5, data.Workouts.Count());
        }

        [Fact]
        public async Task UsersShouldReturnCorrectCollectionWithThreeUsers()
        {
            // Arrange
            var data = DatabaseMock.Instance;
            await data.Users.AddRangeAsync(Enumerable.Range(1, 5).Select(x => new ApplicationUser()
            {
                Country = new Country(),
            }));
            await data.SaveChangesAsync();

            var mapper = MapperMock.Instance;
            var homeService = new HomeService(data, mapper);

            // Act
            var result = await homeService.Users();

            // Assert
            Assert.Equal(3, result.Count());
            Assert.Equal(5, data.Users.Count());
        }

        [Fact]
        public async Task SupplementsShouldReturnCorrectCollectionWithThreeLatestSupplements()
        {
            // Arrange
            var data = DatabaseMock.Instance;
            await data.Supplements.AddRangeAsync(Enumerable.Range(1, 5).Select(x => new Supplement()
            {
                Category = new SupplementCategory(),
            }));
            await data.SaveChangesAsync();

            var mapper = MapperMock.Instance;
            var homeService = new HomeService(data, mapper);

            // Act
            var result = await homeService.Supplements();

            // Assert
            Assert.Equal(3, result.Count());
            Assert.Equal(5, data.Supplements.Count());
        }
    }
}
