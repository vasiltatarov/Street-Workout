namespace StreetWorkout.Test.Services
{
    using Xunit;
    using System.Linq;

    using Data.Models;
    using Data.Models.Enums;
    using Mocks;
    using StreetWorkout.Services.Workouts;
    using StreetWorkout.Services.Workouts.Models;

    public class WorkoutServiceTest
    {
        [Fact]
        public void CreateShouldCreateNewWorkoutCorrectlyAndIncreaseWorkoutsCount()
        {
            // Arrange
            var data = DatabaseMock.Instance;
            var mapper = MapperMock.Instance;

            var workoutService = new WorkoutService(data, mapper);

            // Act
            workoutService.Create("test", 1, DifficultLevel.Advanced, 2, "sad", 1, "");

            // Assert
            Assert.Equal(1, data.Workouts.Count());
        }

        [Fact]
        public void GetSportsShouldReturnEmptyCollection()
        {
            // Arrange
            var data = DatabaseMock.Instance;
            var mapper = MapperMock.Instance;

            var workoutService = new WorkoutService(data, mapper);

            // Act
            var sports = workoutService.GetSports();

            // Assert
            Assert.Empty(sports);
        }

        [Fact]
        public void GetSportsShouldReturnCollectionWithAllSportsFromDatabase()
        {
            // Arrange
            var data = DatabaseMock.Instance;
            data.Sports.AddRange(Enumerable.Range(1, 5).Select(x => new Sport()));
            data.SaveChanges();

            var mapper = MapperMock.Instance;

            var workoutService = new WorkoutService(data, mapper);

            // Act
            var sports = workoutService.GetSports();

            // Assert
            Assert.NotNull(sports);
            Assert.Equal(5, sports.Count());
        }

        [Fact]
        public void GetBodyPartsShouldReturnEmptyCollection()
        {
            // Arrange
            var data = DatabaseMock.Instance;
            var mapper = MapperMock.Instance;

            var workoutService = new WorkoutService(data, mapper);

            // Act
            var bodyParts = workoutService.GetBodyParts();

            // Assert
            Assert.Empty(bodyParts);
        }

        [Fact]
        public void GetBodyPartsShouldReturnCollectionWithAllSportsFromDatabase()
        {
            // Arrange
            var data = DatabaseMock.Instance;
            data.BodyParts.AddRange(Enumerable.Range(1, 5).Select(x => new BodyPart()));
            data.SaveChanges();

            var mapper = MapperMock.Instance;

            var workoutService = new WorkoutService(data, mapper);

            // Act
            var bodyParts = workoutService.GetBodyParts();

            // Assert
            Assert.NotNull(bodyParts);
            Assert.Equal(5, bodyParts.Count());
        }

        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        public void IsValidSportIdShouldReturnTrueWhenSportWithGivenIdNotExistInDatabase(int sportId)
        {
            // Arrange
            var data = DatabaseMock.Instance;
            data.Sports.Add(new Sport { Id = sportId });
            data.SaveChanges();

            var mapper = MapperMock.Instance;

            var workoutService = new WorkoutService(data, mapper);

            // Act
            var result = workoutService.IsValidSportId(sportId);

            // Assert
            Assert.True(result);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        public void IsValidSportIdShouldReturnFalseWhenSportWithGivenIdNotExistInDatabase(int sportId)
        {
            // Arrange
            var data = DatabaseMock.Instance;
            data.Sports.Add(new Sport { Id = sportId });
            data.SaveChanges();

            var mapper = MapperMock.Instance;

            var workoutService = new WorkoutService(data, mapper);

            // Act
            var result = workoutService.IsValidSportId(2);

            // Assert
            Assert.False(result);
        }
        
        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        public void IsValidBodyPartIdShouldReturnTrueWhenSportWithGivenIdNotExistInDatabase(int bodyPartId)
        {
            // Arrange
            var data = DatabaseMock.Instance;
            data.BodyParts.Add(new BodyPart { Id = bodyPartId });
            data.SaveChanges();

            var mapper = MapperMock.Instance;

            var workoutService = new WorkoutService(data, mapper);

            // Act
            var result = workoutService.IsValidBodyPartId(bodyPartId);

            // Assert
            Assert.True(result);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        public void IsValidBodyPartIdShouldReturnFalseWhenSportWithGivenIdNotExistInDatabase(int bodyPartId)
        {
            // Arrange
            var data = DatabaseMock.Instance;
            data.BodyParts.Add(new BodyPart() { Id = bodyPartId });
            data.SaveChanges();

            var mapper = MapperMock.Instance;

            var workoutService = new WorkoutService(data, mapper);

            // Act
            var result = workoutService.IsValidBodyPartId(2);

            // Assert
            Assert.False(result);
        }

        [Theory]
        [InlineData(1)]
        public void DetailsShouldReturnValidServiceModelWithValidData(int workoutId)
        {
            // Arrange
            var data = DatabaseMock.Instance;
            data.Workouts.Add(new Workout
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
            data.SaveChanges();

            var mapper = MapperMock.Instance;

            var workoutService = new WorkoutService(data, mapper);

            // Act
            var result = workoutService.Details(workoutId);

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
        public void DetailsShouldReturnNullWhenWorkoutWithGivenIdNotExist(int workoutId)
        {
            // Arrange
            var data = DatabaseMock.Instance;
            data.Workouts.Add(new Workout
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
            data.SaveChanges();

            var mapper = MapperMock.Instance;

            var workoutService = new WorkoutService(data, mapper);

            // Act
            var result = workoutService.Details(workoutId);

            // Assert
            Assert.Null(result);
        }

        [Theory]
        [InlineData("vs1", 1)]
        public void WorkoutsShouldReturnValidWorkoutModelWithExactlyWorkoutsPerFirstPage(string userId, int currentPage)
        {
            // Arrange
            var data = DatabaseMock.Instance;

            data.Users.Add(new ApplicationUser
            {
                Id = userId,
                UserRole = UserRole.Trainer,
            });
            data.Workouts.AddRange(Enumerable.Range(1, 10).Select(x => new Workout
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
            data.SaveChanges();

            var mapper = MapperMock.Instance;

            var workoutService = new WorkoutService(data, mapper);

            // Act
            var result = workoutService.Workouts(userId, null, null, null, currentPage);

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
        public void WorkoutsShouldReturnValidWorkoutModelWithExactlyWorkoutsPerSecondPage(string userId, int currentPage)
        {
            // Arrange
            var data = DatabaseMock.Instance;

            data.Users.Add(new ApplicationUser
            {
                Id = userId,
                UserRole = UserRole.Trainer,
            });
            data.Workouts.AddRange(Enumerable.Range(1, 12).Select(x => new Workout
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
            data.SaveChanges();

            var mapper = MapperMock.Instance;

            var workoutService = new WorkoutService(data, mapper);

            // Act
            var result = workoutService.Workouts(userId, null, null, null, currentPage);

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
        public void WorkoutsShouldReturnValidWorkoutModelWithZeroWorkoutsWhenInDatabaseNotExistAnyWorkouts(string userId, int currentPage)
        {
            // Arrange
            var data = DatabaseMock.Instance;

            data.Users.Add(new ApplicationUser
            {
                Id = userId,
                UserRole = UserRole.Trainer,
            });
            data.BodyParts.AddRange(Enumerable.Range(1, 10).Select(x => new BodyPart()));
            data.SaveChanges();

            var mapper = MapperMock.Instance;

            var workoutService = new WorkoutService(data, mapper);

            // Act
            var result = workoutService.Workouts(userId, null, null, null, currentPage);

            // Assert
            Assert.IsType<WorkoutsQueryModel>(result);
            Assert.NotNull(result);
            Assert.Empty(result.Workouts);
            Assert.Equal(currentPage, result.CurrentPage);
            Assert.Equal(10, result.BodyParts.Count());
        }
    }
}
