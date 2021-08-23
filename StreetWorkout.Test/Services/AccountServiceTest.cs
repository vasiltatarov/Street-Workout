namespace StreetWorkout.Test.Services
{
    using System.Linq;
    using System.Threading.Tasks;
    using StreetWorkout.Data.Models;
    using StreetWorkout.Services.Accounts;
    using StreetWorkout.Test.Mocks;
    using StreetWorkout.ViewModels.Accounts;
    using Xunit;

    public class AccountServiceTest
    {
        [Theory]
        [InlineData("vs1")]
        public async Task IsUserAccountCompleteShouldReturnTrueWhenUserAccountIsCompleted(string userId)
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
            var result = await accountService.IsUserAccountComplete(userId);

            // Assert
            Assert.True(result);
        }

        [Theory]
        [InlineData("vs1")]
        public async Task IsUserAccountCompleteShouldReturnFalseWhenUserAccountIsNotCompleted(string userId)
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
            var result = await accountService.IsUserAccountComplete(userId);

            // Assert
            Assert.False(result);
        }

        [Theory]
        [InlineData("vs1")]
        public async Task IsUserAccountCompleteShouldReturnFalseWhenUserNotExist(string userId)
        {
            // Arrange
            var data = DatabaseMock.Instance;
            var mapper = MapperMock.Instance;

            var accountService = new AccountService(data, mapper);

            // Act
            var result = await accountService.IsUserAccountComplete(userId);

            // Assert
            Assert.False(result);
        }

        [Theory]
        [InlineData("vs1")]
        public async Task IsUserDataExistsShouldReturnTrueWhenUserDataForGivenUserExist(string userId)
        {
            // Arrange
            var data = DatabaseMock.Instance;
            var mapper = MapperMock.Instance;

            await data.UserDatas.AddAsync(new UserData()
            {
                User = new ApplicationUser
                {
                    Id = userId,
                },
            });
            await data.SaveChangesAsync();

            var accountService = new AccountService(data, mapper);

            // Act
            var result = await accountService.IsUserDataExists(userId);

            // Assert
            Assert.True(result);
        }

        [Theory]
        [InlineData("vs1")]

        public async Task IsUserDataExistsShouldReturnFalseWhenUserDataForGivenUserNotExist(string userId)
        {
            // Arrange
            var data = DatabaseMock.Instance;
            var mapper = MapperMock.Instance;

            var accountService = new AccountService(data, mapper);

            // Act
            var result = await accountService.IsUserDataExists(userId);

            // Assert
            Assert.False(result);
        }

        [Theory]
        [InlineData(2)]
        public async Task IsValidSportIdShouldReturnTrueWhenSportExist(int sportId)
        {
            // Arrange
            var data = DatabaseMock.Instance;
            var mapper = MapperMock.Instance;

            await data.Sports.AddAsync(new Sport
            {
                Id = sportId,
                Name = "test",
            });
            await data.SaveChangesAsync();

            var accountService = new AccountService(data, mapper);

            // Act
            var result = await accountService.IsValidSportId(sportId);

            // Assert
            Assert.True(result);
        }

        [Theory]
        [InlineData(2)]
        public async Task IsValidSportIdShouldReturnFalseWhenSportNotExist(int sportId)
        {
            // Arrange
            var data = DatabaseMock.Instance;
            var mapper = MapperMock.Instance;

            var accountService = new AccountService(data, mapper);

            // Act
            var result = await accountService.IsValidSportId(sportId);

            // Assert
            Assert.False(result);
        }

        [Theory]
        [InlineData(2)]
        public async Task IsValidGoalIdShouldReturnTrueWhenSportExist(int goalId)
        {
            // Arrange
            var data = DatabaseMock.Instance;
            var mapper = MapperMock.Instance;

            await data.Goals.AddAsync(new Goal()
            {
                Id = goalId,
                Name = "test",
            });
            await data.SaveChangesAsync();

            var accountService = new AccountService(data, mapper);

            // Act
            var result = await accountService.IsValidGoalId(goalId);

            // Assert
            Assert.True(result);
        }

        [Theory]
        [InlineData(2)]
        public async Task IsValidGoalIdShouldReturnFalseWhenSportNotExist(int goalId)
        {
            // Arrange
            var data = DatabaseMock.Instance;
            var mapper = MapperMock.Instance;

            var accountService = new AccountService(data, mapper);

            // Act
            var result = await accountService.IsValidGoalId(goalId);

            // Assert
            Assert.False(result);
        }

        [Theory]
        [InlineData(2)]
        public async Task IsValidTrainingFrequencyIdShouldReturnTrueWhenSportExist(int trainingFrequencyId)
        {
            // Arrange
            var data = DatabaseMock.Instance;
            var mapper = MapperMock.Instance;

            await data.TrainingFrequencies.AddAsync(new TrainingFrequency
            {
                Id = trainingFrequencyId,
                Name = "test",
            });
            await data.SaveChangesAsync();

            var accountService = new AccountService(data, mapper);

            // Act
            var result = await accountService.IsValidTrainingFrequencyId(trainingFrequencyId);

            // Assert
            Assert.True(result);
        }

        [Theory]
        [InlineData(2)]
        public async Task IsValidTrainingFrequencyIdShouldReturnFalseWhenSportNotExist(int trainingFrequencyId)
        {
            // Arrange
            var data = DatabaseMock.Instance;
            var mapper = MapperMock.Instance;

            var accountService = new AccountService(data, mapper);

            // Act
            var result = await accountService.IsValidTrainingFrequencyId(trainingFrequencyId);

            // Assert
            Assert.False(result);
        }

        [Theory]
        [InlineData("vs1", 1, 1, 1, 80, 180, "hiiii test")]
        public async Task CompleteAccountShouldAddUserDataAndChangeIsAccountCompletedOnTrue(string userId, int sportId, int goalId, int trainingFrequency, int weight, int height,
            string description)
        {
            // Arrange
            var data = DatabaseMock.Instance;
            var mapper = MapperMock.Instance;

            await data.Users.AddAsync(new ApplicationUser()
            {
                Id = userId,
            });
            await data.SaveChangesAsync();

            var accountService = new AccountService(data, mapper);

            // Act
            await accountService.CompleteAccount(userId, sportId, goalId, trainingFrequency, weight, height, description);
            var user = await data.Users.FindAsync(userId);

            // Assert
            Assert.Equal(1, data.Users.Count());
            Assert.Equal(1, data.UserDatas.Count());
            Assert.True(user.IsAccountCompleted);
        }

        [Theory]
        [InlineData("vasko", "Kirkovo")]
        public async Task GetAccountShouldReturnCorrectAccountViewModelWithDataForGivenUsername(string username, string country)
        {
            // Arrange
            var data = DatabaseMock.Instance;
            var mapper = MapperMock.Instance;

            await data.Users.AddAsync(new ApplicationUser()
            {
                UserName = username,
                Country = new Country
                {
                    Name = country,
                },
            });
            await data.SaveChangesAsync();

            var accountService = new AccountService(data, mapper);

            // Act
            var result = await accountService.GetAccount(username);

            // Assert
            Assert.IsType<AccountViewModel>(result);
            Assert.NotNull(result);
            Assert.Equal(country, result.Country);
            Assert.Equal(username, result.Username);
        }

        [Fact]
        public async Task GetSportsInAccountFormModelShouldReturnCorrectNoTEmptyCollectionOfSports()
        {
            // Arrange
            var data = DatabaseMock.Instance;
            var mapper = MapperMock.Instance;

            await data.Sports.AddRangeAsync(Enumerable.Range(1, 10).Select(x => new Sport()));
            await data.SaveChangesAsync();

            var accountService = new AccountService(data, mapper);

            // Act
            var result = await accountService.GetSportsInAccountFormModel();

            // Assert
            Assert.Equal(10, result.Count());
        }

        [Fact]
        public async Task GetSportsInAccountFormModelShouldReturnEmptyCollectionOfSports()
        {
            // Arrange
            var data = DatabaseMock.Instance;
            var mapper = MapperMock.Instance;

            var accountService = new AccountService(data, mapper);

            // Act
            var result = await accountService.GetSportsInAccountFormModel();

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetGoalsInAccountFormModelShouldReturnCorrectNoTEmptyCollectionOfSports()
        {
            // Arrange
            var data = DatabaseMock.Instance;
            var mapper = MapperMock.Instance;

            await data.Goals.AddRangeAsync(Enumerable.Range(1, 10).Select(x => new Goal()));
            await data.SaveChangesAsync();

            var accountService = new AccountService(data, mapper);

            // Act
            var result = await accountService.GetGoalsInAccountFormModel();

            // Assert
            Assert.Equal(10, result.Count());
        }

        [Fact]
        public async Task GetGoalsInAccountFormModelShouldReturnEmptyCollectionOfSports()
        {
            // Arrange
            var data = DatabaseMock.Instance;
            var mapper = MapperMock.Instance;

            var accountService = new AccountService(data, mapper);

            // Act
            var result = await accountService.GetGoalsInAccountFormModel();

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetTrainingFrequenciesInAccountFormModelShouldReturnCorrectNoTEmptyCollectionOfSports()
        {
            // Arrange
            var data = DatabaseMock.Instance;
            var mapper = MapperMock.Instance;

            await data.TrainingFrequencies.AddRangeAsync(Enumerable.Range(1, 10).Select(x => new TrainingFrequency()));
            await data.SaveChangesAsync();

            var accountService = new AccountService(data, mapper);

            // Act
            var result = await accountService.GetTrainingFrequenciesInAccountFormModel();

            // Assert
            Assert.Equal(10, result.Count());
        }

        [Fact]
        public async Task GetTrainingFrequenciesInAccountFormModelShouldReturnEmptyCollectionOfSports()
        {
            // Arrange
            var data = DatabaseMock.Instance;
            var mapper = MapperMock.Instance;

            var accountService = new AccountService(data, mapper);

            // Act
            var result = await accountService.GetTrainingFrequenciesInAccountFormModel();

            // Assert
            Assert.Empty(result);
        }
    }
}
