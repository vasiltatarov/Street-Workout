namespace StreetWorkout.Test.Services
{
    using Xunit;
    using System.Linq;

    using Data.Models;
    using Data.Models.Enums;
    using StreetWorkout.Services.Trainings;
    using Mocks;
    using ViewModels.Trainers;

    public class TrainerServiceTest
    {
        [Fact]
        public void AllShouldReturnCorrectViewModelWithCorrectCountTrainers()
        {
            // Arrange
            var data = DatabaseMock.Instance;

            data.UserDatas.AddRange(Enumerable.Range(1, 10).Select(x => new UserData
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
            data.SaveChanges();

            var trainerService = new TrainerService(data);

            // Act
            var result = trainerService.All(1);
            var trainers = result.Trainers;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<AllTrainersViewModel>(result);
            Assert.Equal(1, result.CurrentPage);
            Assert.Equal(10, result.TotalTrainers);
            Assert.Equal(9, trainers.Count());
        }

        [Fact]
        public void AllShouldReturnNotZeroCountTrainers()
        {
            // Arrange
            var data = DatabaseMock.Instance;

            data.UserDatas.AddRange(Enumerable.Range(1, 10).Select(x => new UserData
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
            data.SaveChanges();

            var trainerService = new TrainerService(data);

            // Act
            var result = trainerService.All(1);
            var trainers = result.Trainers;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<AllTrainersViewModel>(result);
            Assert.NotEqual(0, result.CurrentPage);
            Assert.NotEqual(0, result.TotalTrainers);
            Assert.NotEmpty(trainers);
        }

        [Fact]
        public void AllShouldReturnZeroCountTrainersWhenTrainersNotExistsInDatabase()
        {
            // Arrange
            var data = DatabaseMock.Instance;

            data.UserDatas.AddRange(Enumerable.Range(1, 10).Select(x => new UserData
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
            data.SaveChanges();

            var trainerService = new TrainerService(data);

            // Act
            var result = trainerService.All(1);
            var trainers = result.Trainers;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<AllTrainersViewModel>(result);
            Assert.Equal(1, result.CurrentPage);
            Assert.Equal(0, result.TotalTrainers);
            Assert.Empty(trainers);
        }

        [Fact]
        public void AllShouldReturnCorrectViewModelWithCorrectCountTrainersWhenCurrentPageIsTwo()
        {
            // Arrange
            var data = DatabaseMock.Instance;

            data.UserDatas.AddRange(Enumerable.Range(1, 13).Select(x => new UserData
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
            data.SaveChanges();

            var trainerService = new TrainerService(data);

            // Act
            var result = trainerService.All(2);
            var trainers = result.Trainers;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<AllTrainersViewModel>(result);
            Assert.Equal(2, result.CurrentPage);
            Assert.Equal(13, result.TotalTrainers);
            Assert.Equal(4, trainers.Count());
        }
    }
}
