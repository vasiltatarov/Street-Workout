namespace StreetWorkout.Test.Services
{
    using System.Linq;
    using Xunit;
    using Data.Models;
    using StreetWorkout.Services.Accounts;
    using ViewModels.Accounts;
    using Mocks;

    public class AccountServiceTest
    {
        [Theory]
        [InlineData("vs1")]
        public void IsUserAccountCompleteShouldReturnTrueWhenUserAccountIsCompleted(string userId)
        {
            // Arrange
            var data = DatabaseMock.Instance;
            var mapper = MapperMock.Instance;

            data.Users.Add(new ApplicationUser
            {
                Id = userId,
                IsAccountCompleted = true,
            });

            var accountService = new AccountService(data, mapper);

            // Act
            var result = accountService.IsUserAccountComplete(userId);

            // Assert
            Assert.True(result);
        }

        [Theory]
        [InlineData("vs1")]
        public void IsUserAccountCompleteShouldReturnFalseWhenUserAccountIsNotCompleted(string userId)
        {
            // Arrange
            var data = DatabaseMock.Instance;
            var mapper = MapperMock.Instance;

            data.Users.Add(new ApplicationUser
            {
                Id = userId,
            });

            var accountService = new AccountService(data, mapper);

            // Act
            var result = accountService.IsUserAccountComplete(userId);

            // Assert
            Assert.False(result);
        }

        [Theory]
        [InlineData("vs1")]
        public void IsUserDataExistsShouldReturnTrueWhenUserDataForGivenUserExist(string userId)
        {
            // Arrange
            var data = DatabaseMock.Instance;
            var mapper = MapperMock.Instance;

            data.UserDatas.Add(new UserData()
            {
                User = new ApplicationUser
                {
                    Id = userId,
                }
            });
            data.SaveChanges();

            var accountService = new AccountService(data, mapper);

            // Act
            var result = accountService.IsUserDataExists(userId);

            // Assert
            Assert.True(result);
        }

        [Theory]
        [InlineData("vs1")]

        public void IsUserDataExistsShouldReturnFalseWhenUserDataForGivenUserNotExist(string userId)
        {
            // Arrange
            var data = DatabaseMock.Instance;
            var mapper = MapperMock.Instance;

            var accountService = new AccountService(data, mapper);

            // Act
            var result = accountService.IsUserDataExists(userId);

            // Assert
            Assert.False(result);
        }

        [Theory]
        [InlineData(2)]
        public void IsValidSportIdShouldReturnTrueWhenSportExist(int sportId)
        {
            // Arrange
            var data = DatabaseMock.Instance;
            var mapper = MapperMock.Instance;

            data.Sports.Add(new Sport
            {
                Id = sportId,
                Name = "test",
            });
            data.SaveChanges();

            var accountService = new AccountService(data, mapper);

            // Act
            var result = accountService.IsValidSportId(sportId);

            // Assert
            Assert.True(result);
        }

        [Theory]
        [InlineData(2)]
        public void IsValidSportIdShouldReturnFalseWhenSportNotExist(int sportId)
        {
            // Arrange
            var data = DatabaseMock.Instance;
            var mapper = MapperMock.Instance;

            var accountService = new AccountService(data, mapper);

            // Act
            var result = accountService.IsValidSportId(sportId);

            // Assert
            Assert.False(result);
        }

        [Theory]
        [InlineData(2)]
        public void IsValidGoalIdShouldReturnTrueWhenSportExist(int goalId)
        {
            // Arrange
            var data = DatabaseMock.Instance;
            var mapper = MapperMock.Instance;

            data.Goals.Add(new Goal()
            {
                Id = goalId,
                Name = "test",
            });
            data.SaveChanges();

            var accountService = new AccountService(data, mapper);

            // Act
            var result = accountService.IsValidGoalId(goalId);

            // Assert
            Assert.True(result);
        }

        [Theory]
        [InlineData(2)]
        public void IsValidGoalIdShouldReturnFalseWhenSportNotExist(int goalId)
        {
            // Arrange
            var data = DatabaseMock.Instance;
            var mapper = MapperMock.Instance;

            var accountService = new AccountService(data, mapper);

            // Act
            var result = accountService.IsValidGoalId(goalId);

            // Assert
            Assert.False(result);
        }

        [Theory]
        [InlineData(2)]
        public void IsValidTrainingFrequencyIdShouldReturnTrueWhenSportExist(int trainingFrequencyId)
        {
            // Arrange
            var data = DatabaseMock.Instance;
            var mapper = MapperMock.Instance;

            data.TrainingFrequencies.Add(new TrainingFrequency
            {
                Id = trainingFrequencyId,
                Name = "test",
            });
            data.SaveChanges();

            var accountService = new AccountService(data, mapper);

            // Act
            var result = accountService.IsValidTrainingFrequencyId(trainingFrequencyId);

            // Assert
            Assert.True(result);
        }

        [Theory]
        [InlineData(2)]
        public void IsValidTrainingFrequencyIdShouldReturnFalseWhenSportNotExist(int trainingFrequencyId)
        {
            // Arrange
            var data = DatabaseMock.Instance;
            var mapper = MapperMock.Instance;

            var accountService = new AccountService(data, mapper);

            // Act
            var result = accountService.IsValidTrainingFrequencyId(trainingFrequencyId);

            // Assert
            Assert.False(result);
        }

        [Theory]
        [InlineData("vs1", 1, 1, 1, 80, 180, "hiiii test")]
        public void CompleteAccountShouldAddUserDataAndChangeIsAccountCompletedOnTrue(string userId, int sportId, int goalId, int trainingFrequency, int weight, int height,
            string description)
        {
            // Arrange
            var data = DatabaseMock.Instance;
            var mapper = MapperMock.Instance;
            
            data.Users.Add(new ApplicationUser()
            {
                Id = userId,
            });
            data.SaveChanges();

            var accountService = new AccountService(data, mapper);

            // Act
            accountService.CompleteAccount(userId, sportId, goalId, trainingFrequency, weight, height, description);
            var user = data.Users.Find(userId);

            // Assert
            Assert.Equal(1, data.Users.Count());
            Assert.Equal(1, data.UserDatas.Count());
            Assert.True(user.IsAccountCompleted);
        }

        [Theory]
        [InlineData("vasko", "Kirkovo")]
        public void GetAccountShouldReturnCorrectAccountViewModelWithDataForGivenUsername(string username, string country)
        {
            // Arrange
            var data = DatabaseMock.Instance;
            var mapper = MapperMock.Instance;

            data.Users.Add(new ApplicationUser()
            {
                UserName = username,
                Country = new Country
                {
                    Name = country,
                },
            });
            data.SaveChanges();

            var accountService = new AccountService(data, mapper);

            // Act
            var result = accountService.GetAccount(username);

            // Assert
            Assert.IsType<AccountViewModel>(result);
            Assert.NotNull(result);
            Assert.Equal(country, result.Country);
            Assert.Equal(username, result.Username);
        }

        [Fact]    
        public void GetSportsInAccountFormModelShouldReturnCorrectNoTEmptyCollectionOfSports()
        {
            // Arrange
            var data = DatabaseMock.Instance;
            var mapper = MapperMock.Instance;

            data.Sports.AddRange(Enumerable.Range(1, 10).Select(x => new Sport()));
            data.SaveChanges();

            var accountService = new AccountService(data, mapper);

            // Act
            var result = accountService.GetSportsInAccountFormModel();

            // Assert
            Assert.Equal(10, result.Count());
        }

        [Fact]
        public void GetSportsInAccountFormModelShouldReturnEmptyCollectionOfSports()
        {
            // Arrange
            var data = DatabaseMock.Instance;
            var mapper = MapperMock.Instance;

            var accountService = new AccountService(data, mapper);

            // Act
            var result = accountService.GetSportsInAccountFormModel();

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void GetGoalsInAccountFormModelShouldReturnCorrectNoTEmptyCollectionOfSports()
        {
            // Arrange
            var data = DatabaseMock.Instance;
            var mapper = MapperMock.Instance;

            data.Goals.AddRange(Enumerable.Range(1, 10).Select(x => new Goal()));
            data.SaveChanges();

            var accountService = new AccountService(data, mapper);

            // Act
            var result = accountService.GetGoalsInAccountFormModel();

            // Assert
            Assert.Equal(10, result.Count());
        }

        [Fact]
        public void GetGoalsInAccountFormModelShouldReturnEmptyCollectionOfSports()
        {
            // Arrange
            var data = DatabaseMock.Instance;
            var mapper = MapperMock.Instance;

            var accountService = new AccountService(data, mapper);

            // Act
            var result = accountService.GetGoalsInAccountFormModel();

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void GetTrainingFrequenciesInAccountFormModelShouldReturnCorrectNoTEmptyCollectionOfSports()
        {
            // Arrange
            var data = DatabaseMock.Instance;
            var mapper = MapperMock.Instance;

            data.TrainingFrequencies.AddRange(Enumerable.Range(1, 10).Select(x => new TrainingFrequency()));
            data.SaveChanges();

            var accountService = new AccountService(data, mapper);

            // Act
            var result = accountService.GetTrainingFrequenciesInAccountFormModel();

            // Assert
            Assert.Equal(10, result.Count());
        }

        [Fact]
        public void GetTrainingFrequenciesInAccountFormModelShouldReturnEmptyCollectionOfSports()
        {
            // Arrange
            var data = DatabaseMock.Instance;
            var mapper = MapperMock.Instance;

            var accountService = new AccountService(data, mapper);

            // Act
            var result = accountService.GetTrainingFrequenciesInAccountFormModel();

            // Assert
            Assert.Empty(result);
        }
    }
}
