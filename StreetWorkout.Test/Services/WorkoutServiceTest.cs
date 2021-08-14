namespace StreetWorkout.Test.Services
{
    using System.Linq;
    using System.Threading.Tasks;
    using Xunit;

    using StreetWorkout.Data.Models;
    using StreetWorkout.Data.Models.Enums;
    using Mocks;
    using StreetWorkout.Services.Workouts;
    using StreetWorkout.Services.Workouts.Models;

    public class WorkoutServiceTest
    {
        [Fact]
        public async Task CreateShouldCreateNewWorkoutCorrectlyAndIncreaseWorkoutsCount()
        {
            // Arrange
            var data = DatabaseMock.Instance;
            var mapper = MapperMock.Instance;

            var workoutService = new WorkoutService(data, mapper);

            // Act
            await workoutService.Create("test", 1, DifficultLevel.Advanced, 2, "sad", 1, "");

            // Assert
            Assert.Equal(1, data.Workouts.Count());
        }

        [Fact]
        public async Task GetSportsShouldReturnEmptyCollection()
        {
            // Arrange
            var data = DatabaseMock.Instance;
            var mapper = MapperMock.Instance;

            var workoutService = new WorkoutService(data, mapper);

            // Act
            var sports = await workoutService.GetSports();

            // Assert
            Assert.Empty(sports);
        }

        [Fact]
        public async Task GetSportsShouldReturnCollectionWithAllSportsFromDatabase()
        {
            // Arrange
            var data = DatabaseMock.Instance;
            await data.Sports.AddRangeAsync(Enumerable.Range(1, 5).Select(x => new Sport()));
            await data.SaveChangesAsync();

            var mapper = MapperMock.Instance;

            var workoutService = new WorkoutService(data, mapper);

            // Act
            var sports = await workoutService.GetSports();

            // Assert
            Assert.NotNull(sports);
            Assert.Equal(5, sports.Count());
        }

        [Fact]
        public async Task GetBodyPartsShouldReturnEmptyCollection()
        {
            // Arrange
            var data = DatabaseMock.Instance;
            var mapper = MapperMock.Instance;

            var workoutService = new WorkoutService(data, mapper);

            // Act
            var bodyParts = await workoutService.GetBodyParts();

            // Assert
            Assert.Empty(bodyParts);
        }

        [Fact]
        public async Task GetBodyPartsShouldReturnCollectionWithAllSportsFromDatabase()
        {
            // Arrange
            var data = DatabaseMock.Instance;
            await data.BodyParts.AddRangeAsync(Enumerable.Range(1, 5).Select(x => new BodyPart()));
            await data.SaveChangesAsync();

            var mapper = MapperMock.Instance;

            var workoutService = new WorkoutService(data, mapper);

            // Act
            var bodyParts = await workoutService.GetBodyParts();

            // Assert
            Assert.NotNull(bodyParts);
            Assert.Equal(5, bodyParts.Count());
        }

        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        public async Task IsValidSportIdShouldReturnTrueWhenSportWithGivenIdNotExistInDatabase(int sportId)
        {
            // Arrange
            var data = DatabaseMock.Instance;
            await data.Sports.AddAsync(new Sport { Id = sportId });
            await data.SaveChangesAsync();

            var mapper = MapperMock.Instance;

            var workoutService = new WorkoutService(data, mapper);

            // Act
            var result = await workoutService.IsValidSportId(sportId);

            // Assert
            Assert.True(result);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        public async Task IsValidSportIdShouldReturnFalseWhenSportWithGivenIdNotExistInDatabase(int sportId)
        {
            // Arrange
            var data = DatabaseMock.Instance;
            await data.Sports.AddAsync(new Sport { Id = sportId });
            await data.SaveChangesAsync();

            var mapper = MapperMock.Instance;

            var workoutService = new WorkoutService(data, mapper);

            // Act
            var result = await workoutService.IsValidSportId(2);

            // Assert
            Assert.False(result);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        public async Task IsValidBodyPartIdShouldReturnTrueWhenSportWithGivenIdNotExistInDatabase(int bodyPartId)
        {
            // Arrange
            var data = DatabaseMock.Instance;
            await data.BodyParts.AddRangeAsync(new BodyPart { Id = bodyPartId });
            await data.SaveChangesAsync();

            var mapper = MapperMock.Instance;

            var workoutService = new WorkoutService(data, mapper);

            // Act
            var result = await workoutService.IsValidBodyPartId(bodyPartId);

            // Assert
            Assert.True(result);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        public async Task IsValidBodyPartIdShouldReturnFalseWhenSportWithGivenIdNotExistInDatabase(int bodyPartId)
        {
            // Arrange
            var data = DatabaseMock.Instance;
            await data.BodyParts.AddAsync(new BodyPart() { Id = bodyPartId });
            await data.SaveChangesAsync();

            var mapper = MapperMock.Instance;

            var workoutService = new WorkoutService(data, mapper);

            // Act
            var result = await workoutService.IsValidBodyPartId(2);

            // Assert
            Assert.False(result);
        }

        [Theory]
        [InlineData(1)]
        public async Task DetailsShouldReturnValidServiceModelWithValidData(int workoutId)
        {
            // Arrange
            var data = DatabaseMock.Instance;
            await data.Workouts.AddAsync(new Workout
            {
                Id = workoutId,
                Title = "test",
                User = new ApplicationUser
                {
                    Id = "sv1",
                    UserName = "vasko",
                },
                Sport = new Sport
                {
                    Name = "sport"
                },
                BodyPart = new BodyPart
                {
                    Name = "bdp"
                },
                Content = "Have Content"
            });
            await data.SaveChangesAsync();

            var mapper = MapperMock.Instance;

            var workoutService = new WorkoutService(data, mapper);

            // Act
            var result = await workoutService.Details(workoutId);

            // Assert
            Assert.IsType<WorkoutDetailsServiceModel>(result);
            Assert.NotNull(result);
            Assert.Equal(workoutId, result.Id);
            Assert.Equal("vasko", result.UserUsername);
            Assert.Equal("Have Content", result.Content);
        }

        [Theory]
        [InlineData(11)]
        [InlineData(22)]
        public async Task DetailsShouldReturnNullWhenWorkoutWithGivenIdNotExist(int workoutId)
        {
            // Arrange
            var data = DatabaseMock.Instance;
            await data.Workouts.AddAsync(new Workout
            {
                Title = "test",
                User = new ApplicationUser
                {
                    Id = "sv1",
                    UserName = "vasko",
                },
                Sport = new Sport
                {
                    Name = "sport"
                },
                BodyPart = new BodyPart
                {
                    Name = "bdp"
                },
                Content = "Have Content"
            });
            await data.SaveChangesAsync();

            var mapper = MapperMock.Instance;

            var workoutService = new WorkoutService(data, mapper);

            // Act
            var result = await workoutService.Details(workoutId);

            // Assert
            Assert.Null(result);
        }

        [Theory]
        [InlineData("vs1", 1)]
        public async Task WorkoutsShouldReturnValidWorkoutModelWithExactlyWorkoutsPerFirstPage(string userId, int currentPage)
        {
            // Arrange
            var data = DatabaseMock.Instance;

            await data.Users.AddAsync(new ApplicationUser
            {
                Id = userId,
                UserRole = UserRole.Trainer,
            });
            await data.Workouts.AddRangeAsync(Enumerable.Range(1, 10).Select(x => new Workout
            {
                Sport = new Sport
                {
                    Name = "test"
                },
                BodyPart = new BodyPart
                {
                    Name = "test"
                },
                UserId = userId,
            }));
            await data.SaveChangesAsync();

            var mapper = MapperMock.Instance;

            var workoutService = new WorkoutService(data, mapper);

            // Act
            var result = await workoutService.All(userId, null, null, null, currentPage);

            // Assert
            Assert.IsType<WorkoutsQueryModel>(result);
            Assert.NotNull(result);
            Assert.Equal(9, result.Workouts.Count());
            Assert.Equal(currentPage, result.CurrentPage);
            Assert.Equal(10, result.Sports.Count());
            Assert.Equal(10, result.BodyParts.Count());
        }

        [Theory]
        [InlineData("vs1", 2)]
        public async Task WorkoutsShouldReturnValidWorkoutModelWithExactlyWorkoutsPerSecondPage(string userId, int currentPage)
        {
            // Arrange
            var data = DatabaseMock.Instance;

            await data.Users.AddAsync(new ApplicationUser
            {
                Id = userId,
                UserRole = UserRole.Trainer,
            });
            await data.Workouts.AddRangeAsync(Enumerable.Range(1, 12).Select(x => new Workout
            {
                Sport = new Sport
                {
                    Name = "test"
                },
                BodyPart = new BodyPart
                {
                    Name = "test"
                },
                UserId = userId,
            }));
            await data.SaveChangesAsync();

            var mapper = MapperMock.Instance;

            var workoutService = new WorkoutService(data, mapper);

            // Act
            var result = await workoutService.All(userId, null, null, null, currentPage);

            // Assert
            Assert.IsType<WorkoutsQueryModel>(result);
            Assert.NotNull(result);
            Assert.Equal(3, result.Workouts.Count());
            Assert.Equal(currentPage, result.CurrentPage);
            Assert.Equal(12, result.Sports.Count());
            Assert.Equal(12, result.BodyParts.Count());
        }

        [Theory]
        [InlineData("vs1", 1)]
        [InlineData("vs1", 2)]
        [InlineData("vs1", 5)]
        public async Task WorkoutsShouldReturnValidWorkoutModelWithZeroWorkoutsWhenInDatabaseNotExistAnyWorkouts(string userId, int currentPage)
        {
            // Arrange
            var data = DatabaseMock.Instance;

            await data.Users.AddAsync(new ApplicationUser
            {
                Id = userId,
                UserRole = UserRole.Trainer,
            });
            await data.BodyParts.AddRangeAsync(Enumerable.Range(1, 10).Select(x => new BodyPart()));
            await data.SaveChangesAsync();

            var mapper = MapperMock.Instance;

            var workoutService = new WorkoutService(data, mapper);

            // Act
            var result = await workoutService.All(userId, null, null, null, currentPage);

            // Assert
            Assert.IsType<WorkoutsQueryModel>(result);
            Assert.NotNull(result);
            Assert.Empty(result.Workouts);
            Assert.Equal(currentPage, result.CurrentPage);
            Assert.Equal(10, result.BodyParts.Count());
        }
    }
}
