namespace StreetWorkout.Test.Services
{
    using System.Linq;
    using StreetWorkout.Services.Votes;
    using StreetWorkout.Test.Mocks;
    using StreetWorkout.ViewModels.Votes;
    using Xunit;

    public class VoteServiceTest
    {
        [Theory]
        [InlineData("vs1", "sv2", 2)]
        public void SetVoteShouldAddNewVoteToDatabase(string userId, string votedUserId, byte value)
        {
            // Arrange
            var data = DatabaseMock.Instance;
            var voteService = new VoteService(data);

            // Act
            voteService.SetVote(userId, votedUserId, value);

            // Assert
            Assert.Equal(1, data.Votes.Count());
        }

        [Theory]
        [InlineData("vs1", "sv2", 2)]
        public void SetVoteShouldChangeValueOfVoteIfVoteAlreadyExist(string userId, string votedUserId, byte value)
        {
            // Arrange
            var data = DatabaseMock.Instance;
            var voteService = new VoteService(data);

            // Act
            voteService.SetVote(userId, votedUserId, value);
            voteService.SetVote(userId, votedUserId, 5);

            // Assert
            Assert.Equal(1, data.Votes.Count());
            Assert.Equal(5, data.Votes.First().Value);
        }

        [Theory]
        [InlineData("vs1")]
        public void GetAverageVotesShouldReturnZeroIfVotesForThisUserNotExists(string userId)
        {
            // Arrange
            var data = DatabaseMock.Instance;
            var voteService = new VoteService(data);

            // Act
            var result = voteService.GetAverageVotes(userId);

            // Assert
            Assert.IsType<VoteResponseModel>(result);
            Assert.Equal(0, result.AverageVotes);
        }

        [Theory]
        [InlineData("vs1")]
        public void GetAverageVotesShouldReturnCorrectAverageVoteForGivenUser(string userId)
        {
            // Arrange
            var data = DatabaseMock.Instance;
            var voteService = new VoteService(data);

            // Act
            voteService.SetVote(userId, "aa", 3);
            voteService.SetVote(userId, "ss", 5);
            var result = voteService.GetAverageVotes(userId);

            // Assert
            Assert.IsType<VoteResponseModel>(result);
            Assert.Equal(4, result.AverageVotes);
        }
    }
}
