namespace StreetWorkout.Test.Services
{
    using System;
    using System.Linq;
    using Xunit;
    using StreetWorkout.Data.Models;
    using StreetWorkout.Data.Models.Enums;
    using StreetWorkout.Services.GroupWorkouts;
    using StreetWorkout.Services.GroupWorkouts.Models;
    using Mocks;

    public class GroupWorkoutServiceTest
    {
        [Theory]
        [InlineData("vs1")]
        public void IsUserTrainerShouldReturnTrueWhenUserWithGivenUsernameIsTrainer(string userId)
        {
            // Arrange
            var data = DatabaseMock.Instance;

            data.Users.Add(new ApplicationUser
            {
                Id = userId,
                UserRole = UserRole.Trainer,
            });
            data.SaveChanges();

            var mapper = MapperMock.Instance;

            var accountService = new GroupWorkoutService(data, mapper);

            // Act
            var result = accountService.IsUserTrainer(userId);

            // Assert
            Assert.True(result);
        }

        [Theory]
        [InlineData("vs1")]
        public void IsUserTrainerShouldReturnFalseWhenUserWithGivenUsernameIsNotTrainer(string userId)
        {
            // Arrange
            var data = DatabaseMock.Instance;
            var mapper = MapperMock.Instance;
            var accountService = new GroupWorkoutService(data, mapper);

            // Act
            var result = accountService.IsUserTrainer(userId);

            // Assert
            Assert.False(result);
        }

        [Theory]
        [InlineData("title 1", 1, "tuka", 12, 1, "vs1", "test")]
        public void CreateShouldCreateNewGroupWorkoutSuccessfully(string title, int sportId, string address, byte maximumParticipants, byte pricePerPerson, string trainerId, string content)
        {
            // Arrange
            var data = DatabaseMock.Instance;
            var mapper = MapperMock.Instance;

            var accountService = new GroupWorkoutService(data, mapper);

            // Act
            accountService.Create(title, sportId, address, DateTime.Now, DateTime.Now, maximumParticipants, pricePerPerson, trainerId, content);
            var groupWorkout = data.GroupWorkouts.FirstOrDefault();

            // Assert
            Assert.NotNull(groupWorkout);
            Assert.Equal(1, data.GroupWorkouts.Count());
            Assert.Equal(title, groupWorkout.Title);
            Assert.Equal(content, groupWorkout.Content);
        }

        [Theory]
        [InlineData(1, "title 1", 1, "tuka", 12, 2, "vs1", "test")]
        public void AvailableTicketsShouldReturnCorrectAvailableTicketsValueWhenNobodyBuyTicketsYet(int groupWorkoutId, string title, int sportId, string address, byte maximumParticipants, byte pricePerPerson, string trainerId, string content)
        {
            // Arrange
            var data = DatabaseMock.Instance;
            data.GroupWorkouts.Add(new GroupWorkout
            {
                Id = groupWorkoutId,
                Title = title,
                SportId = sportId,
                Address = address,
                StartOn = DateTime.Now,
                EndOn = DateTime.Now,
                MaximumParticipants = maximumParticipants,
                PricePerPerson = pricePerPerson,
                TrainerId = trainerId,
                Content = content,
                CreatedOn = DateTime.UtcNow,
            });
            data.SaveChanges();

            var mapper = MapperMock.Instance;

            var accountService = new GroupWorkoutService(data, mapper);

            // Act
            var result = accountService.AvailableTickets(groupWorkoutId);

            // Assert
            Assert.Equal(maximumParticipants, result);
        }

        [Theory]
        [InlineData(1, "title 1", 1, "tuka", 12, 2, "vs1", "test")]
        public void AvailableTicketsShouldReturnCorrectAvailableTicketsValueWhenSomebodyBuyTickets(int groupWorkoutId, string title, int sportId, string address, byte maximumParticipants, byte pricePerPerson, string trainerId, string content)
        {
            // Arrange
            var data = DatabaseMock.Instance;
            data.GroupWorkouts.Add(new GroupWorkout
            {
                Id = groupWorkoutId,
                Title = title,
                SportId = sportId,
                Address = address,
                StartOn = DateTime.Now,
                EndOn = DateTime.Now,
                MaximumParticipants = maximumParticipants,
                PricePerPerson = pricePerPerson,
                TrainerId = trainerId,
                Content = content,
                CreatedOn = DateTime.UtcNow,
            });
            data.UserWorkoutPayments.Add(new UserWorkoutPayment
            {
                GroupWorkoutId = groupWorkoutId,
                BoughtTickets = 2,
            });
            data.SaveChanges();

            var mapper = MapperMock.Instance;

            var accountService = new GroupWorkoutService(data, mapper);

            // Act
            var result = accountService.AvailableTickets(groupWorkoutId);

            // Assert
            Assert.Equal(10, result);
        }

        [Theory]
        [InlineData(1, "title 1", 1, "tuka", 12, 2, "vs1", "test")]
        public void AvailableTicketsShouldBeZeroWhenAllTicketsAreSold(int groupWorkoutId, string title, int sportId, string address, byte maximumParticipants, byte pricePerPerson, string trainerId, string content)
        {
            // Arrange
            var data = DatabaseMock.Instance;
            data.GroupWorkouts.Add(new GroupWorkout
            {
                Id = groupWorkoutId,
                Title = title,
                SportId = sportId,
                Address = address,
                StartOn = DateTime.Now,
                EndOn = DateTime.Now,
                MaximumParticipants = maximumParticipants,
                PricePerPerson = pricePerPerson,
                TrainerId = trainerId,
                Content = content,
                CreatedOn = DateTime.UtcNow,
            });
            data.UserWorkoutPayments.Add(new UserWorkoutPayment
            {
                GroupWorkoutId = groupWorkoutId,
                BoughtTickets = 12,
            });
            data.SaveChanges();

            var mapper = MapperMock.Instance;

            var accountService = new GroupWorkoutService(data, mapper);

            // Act
            var result = accountService.AvailableTickets(groupWorkoutId);

            // Assert
            Assert.Equal(0, result);
        }

        [Theory]
        [InlineData("vs1", 1, "vt", "089", "22 33 33", 2)]
        public void BuyTicketShouldAddNewUserWorkoutPaymentToDatabase(string userId, int groupWorkoutId, string fullName, string phoneNumber, string card, byte boughtTickets)
        {
            // Arrange
            var data = DatabaseMock.Instance;
            var mapper = MapperMock.Instance;

            var accountService = new GroupWorkoutService(data, mapper);

            // Act
            accountService.BuyTicket(userId, groupWorkoutId, fullName, phoneNumber, card, boughtTickets);
            var payment = data.UserWorkoutPayments.FirstOrDefault();

            // Assert
            Assert.NotNull(payment);
            Assert.Equal(1, data.UserWorkoutPayments.Count());
            Assert.Equal(userId, payment.UserId);
            Assert.Equal(fullName, payment.FullName);
            Assert.Equal(card, payment.Card);
            Assert.Equal(boughtTickets, payment.BoughtTickets);
        }

        [Theory]
        [InlineData(1, "title 1", "tuka", 12, 2, "test")]
        public void DetailsShouldReturnCorrectGroupWorkoutDetailsModelWithDetailsForGivenGroupWorkoutId(int groupWorkoutId, string title, string address, byte maximumParticipants, byte pricePerPerson, string content)
        {
            // Arrange
            var data = DatabaseMock.Instance;
            data.GroupWorkouts.Add(new GroupWorkout
            {
                Id = groupWorkoutId,
                Title = title,
                Sport = new Sport(),
                Address = address,
                StartOn = DateTime.Now,
                EndOn = DateTime.Now,
                MaximumParticipants = maximumParticipants,
                PricePerPerson = pricePerPerson,
                Trainer = new ApplicationUser(),
                Content = content,
                CreatedOn = DateTime.UtcNow,
            });
            data.SaveChanges();

            var mapper = MapperMock.Instance;

            var accountService = new GroupWorkoutService(data, mapper);

            // Act
            var result = accountService.Details(groupWorkoutId);

            // Assert
            Assert.IsType<GroupWorkoutDetailsModel>(result);
            Assert.NotNull(result);
            Assert.Equal(title, result.Title);
            Assert.Equal(maximumParticipants, result.MaximumParticipants);
            Assert.Equal(pricePerPerson, result.PricePerPerson);
            Assert.Equal(content, result.Content);
        }

        [Theory]
        [InlineData(1)]
        public void DetailsShouldReturnNullWhenGroupWorkoutNotExist(int groupWorkoutId)
        {
            // Arrange
            var data = DatabaseMock.Instance;
            var mapper = MapperMock.Instance;

            var accountService = new GroupWorkoutService(data, mapper);

            // Act
            var result = accountService.Details(groupWorkoutId);

            // Assert
            Assert.Null(result);
        }

        [Theory]
        [InlineData(1, "vs1")]
        public void AllShouldReturnCorrectGroupWorkoutsQueryModelWithExactlyWorkoutsPerFirstPageAndCheckIfUserIsTrainer(int currentPage, string userId)
        {
            // Arrange
            var data = DatabaseMock.Instance;

            data.Users.Add(new ApplicationUser
            {
                Id = userId,
                UserRole = UserRole.Trainer,
            });
            data.GroupWorkouts.AddRange(Enumerable.Range(1, 10).Select(x => new GroupWorkout
            {
                Sport = new Sport
                {
                    Name = "test",
                },
                Trainer = new ApplicationUser()
            }));
            data.SaveChanges();

            var mapper = MapperMock.Instance;

            var accountService = new GroupWorkoutService(data, mapper);

            // Act
            var result = accountService.All(currentPage, userId);
            var user = data.Users.Find(userId);

            // Assert
            Assert.IsType<GroupWorkoutsQueryModel>(result);
            Assert.NotNull(result);
            Assert.True(user.UserRole == UserRole.Trainer);
            Assert.Equal(currentPage, result.CurrentPage);
            Assert.Equal(9, result.TotalGroupWorkouts);
            Assert.Equal(9, result.GroupWorkouts.Count());
        }

        [Theory]
        [InlineData(2, "vs1")]
        public void AllShouldReturnCorrectGroupWorkoutsQueryModelWithExactlyWorkoutPerSecondPageAndCheckIfUserIsTrainer(int currentPage, string userId)
        {
            // Arrange
            var data = DatabaseMock.Instance;

            data.Users.Add(new ApplicationUser
            {
                Id = userId,
                UserRole = UserRole.Trainer,
            });
            data.GroupWorkouts.AddRange(Enumerable.Range(1, 10).Select(x => new GroupWorkout
            {
                Sport = new Sport
                {
                    Name = "test",
                },
                Trainer = new ApplicationUser()
            }));
            data.SaveChanges();

            var mapper = MapperMock.Instance;

            var accountService = new GroupWorkoutService(data, mapper);

            // Act
            var result = accountService.All(currentPage, userId);
            var user = data.Users.Find(userId);

            // Assert
            Assert.IsType<GroupWorkoutsQueryModel>(result);
            Assert.NotNull(result);
            Assert.True(user.UserRole == UserRole.Trainer);
            Assert.Equal(currentPage, result.CurrentPage);
            Assert.Equal(1, result.TotalGroupWorkouts);
            Assert.Single(result.GroupWorkouts);
        }

        [Theory]
        [InlineData(1, "vs1")]
        public void AllShouldReturnCorrectGroupWorkoutsQueryModelWithZeroWorkoutPerPageWhenDatabaseNotExistWorkoutsAndCheckIfUserIsTrainer(int currentPage, string userId)
        {
            // Arrange
            var data = DatabaseMock.Instance;

            data.Users.Add(new ApplicationUser
            {
                Id = userId,
                UserRole = UserRole.Trainer,
            });
            data.SaveChanges();

            var mapper = MapperMock.Instance;

            var accountService = new GroupWorkoutService(data, mapper);

            // Act
            var result = accountService.All(currentPage, userId);
            var user = data.Users.Find(userId);

            // Assert
            Assert.IsType<GroupWorkoutsQueryModel>(result);
            Assert.NotNull(result);
            Assert.True(user.UserRole == UserRole.Trainer);
            Assert.Equal(currentPage, result.CurrentPage);
            Assert.Equal(0, result.TotalGroupWorkouts);
            Assert.Empty(result.GroupWorkouts);
        }
    }
}
