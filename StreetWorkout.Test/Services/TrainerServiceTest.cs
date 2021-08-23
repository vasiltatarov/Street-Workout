namespace StreetWorkout.Test.Services
{
    using System.Linq;
    using System.Threading.Tasks;
    using StreetWorkout.Data.Models;
    using StreetWorkout.Data.Models.Enums;
    using StreetWorkout.Services.Trainings;
    using StreetWorkout.Services.Workouts;
    using StreetWorkout.Test.Mocks;
    using StreetWorkout.ViewModels.Trainers;
    using Xunit;

    public class TrainerServiceTest
    {
        [Fact]
        public async Task AllShouldReturnCorrectViewModelWithCorrectCountTrainers()
        {
            // Arrange
            var data = DatabaseMock.Instance;

            await data.UserDatas.AddRangeAsync(Enumerable.Range(1, 10).Select(x => new UserData
            {
                User = new ApplicationUser
                {
                    UserRole = UserRole.Trainer,
                },
                Sport = new Sport
                {
                    Name = "asd",
                },
                Goal = new Goal
                {
                    Name = "sdfs",
                },
            }));
            await data.SaveChangesAsync();

            var mapper = MapperMock.Instance;
            var workoutService = new WorkoutService(data, mapper);

            var trainerService = new TrainerService(data, workoutService);

            // Act
            var result = await trainerService.All(1, string.Empty, string.Empty);
            var trainers = result.Users;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<AllUsersQueryModel>(result);
            Assert.Equal(1, result.CurrentPage);
            Assert.Equal(10, result.TotalUsers);
            Assert.Equal(9, trainers.Count());
        }

        [Fact]
        public async Task AllShouldReturnNotZeroCountTrainers()
        {
            // Arrange
            var data = DatabaseMock.Instance;

            await data.UserDatas.AddRangeAsync(Enumerable.Range(1, 10).Select(x => new UserData
            {
                User = new ApplicationUser
                {
                    UserRole = UserRole.Trainer,
                },
                Sport = new Sport
                {
                    Name = "asd",
                },
                Goal = new Goal
                {
                    Name = "sdfs",
                },
            }));
            await data.SaveChangesAsync();

            var mapper = MapperMock.Instance;
            var workoutService = new WorkoutService(data, mapper);

            var trainerService = new TrainerService(data, workoutService);

            // Act
            var result = await trainerService.All(1, string.Empty, string.Empty);
            var trainers = result.Users;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<AllUsersQueryModel>(result);
            Assert.NotEqual(0, result.CurrentPage);
            Assert.NotEqual(0, result.TotalUsers);
            Assert.NotEmpty(trainers);
        }

        [Fact]
        public async Task AllShouldReturnZeroCountTrainersWhenTrainersNotExistsInDatabase()
        {
            // Arrange
            var data = DatabaseMock.Instance;

            await data.UserDatas.AddRangeAsync(Enumerable.Range(1, 10).Select(x => new UserData
            {
                User = new ApplicationUser
                {
                    UserRole = UserRole.Enthusiast,
                },
                Sport = new Sport
                {
                    Name = "asd",
                },
                Goal = new Goal
                {
                    Name = "sdfs",
                },
            }));
            await data.SaveChangesAsync();

            var mapper = MapperMock.Instance;
            var workoutService = new WorkoutService(data, mapper);

            var trainerService = new TrainerService(data, workoutService);

            // Act
            var result = await trainerService.All(1, string.Empty, string.Empty);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<AllUsersQueryModel>(result);
            Assert.Equal(1, result.CurrentPage);
            Assert.Equal(10, result.TotalUsers);
        }

        [Fact]
        public async Task AllShouldReturnCorrectViewModelWithCorrectCountTrainersWhenCurrentPageIsTwo()
        {
            // Arrange
            var data = DatabaseMock.Instance;

            await data.UserDatas.AddRangeAsync(Enumerable.Range(1, 13).Select(x => new UserData
            {
                User = new ApplicationUser
                {
                    UserRole = UserRole.Trainer,
                },
                Sport = new Sport
                {
                    Name = "asd",
                },
                Goal = new Goal
                {
                    Name = "sdfs",
                },
            }));
            await data.SaveChangesAsync();

            var mapper = MapperMock.Instance;
            var workoutService = new WorkoutService(data, mapper);

            var trainerService = new TrainerService(data, workoutService);

            // Act
            var result = await trainerService.All(2, string.Empty, string.Empty);
            var trainers = result.Users;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<AllUsersQueryModel>(result);
            Assert.Equal(2, result.CurrentPage);
            Assert.Equal(13, result.TotalUsers);
            Assert.Equal(4, trainers.Count());
        }
    }
}
