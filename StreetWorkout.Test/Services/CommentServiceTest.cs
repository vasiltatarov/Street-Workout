namespace StreetWorkout.Test.Services
{
    using System.Linq;
    using Xunit;
    using StreetWorkout.Data.Models;
    using Mocks;
    using StreetWorkout.Services.Comments;
    using ViewModels.Comments;

    public class CommentServiceTest
    {
        [Theory]
        [InlineData("test", "vs1", 1)]
        [InlineData("test1", "vs2", 3)]
        public void AddMethodShouldAddNewCommentToDatabase(string content, string userId, int workoutId)
        {
            const string username = "vasko";
            const string imageUrl = "vasko.png";

            // Arrange
            var data = DatabaseMock.Instance;
            data.Users.Add(new ApplicationUser
            {
                Id = userId,
                UserName = username,
                ImageUrl = imageUrl,
            });
            data.SaveChanges();

            var commentService = new CommentService(data);

            // Act
            var result = commentService.Add(content, userId, workoutId);

            // Assert
            Assert.IsType<CommentResponseModel>(result);
            Assert.NotNull(result);
            Assert.Equal(1, data.Comments.Count());
            Assert.Equal(username, result.UserUsername);
            Assert.Equal(imageUrl, result.UserImageUrl);
            Assert.Equal(content, result.Content);
        }
    }
}
