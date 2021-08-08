using MyTested.AspNetCore.Mvc;
using StreetWorkout.Controllers.Api;
using StreetWorkout.Data.Models;
using StreetWorkout.ViewModels.Comments;
using Xunit;

namespace StreetWorkout.Test.Controllers.Api
{
    public class CommentsApiControllerTest
    {
        [Fact]
        public void AddShouldAddNewCommentCorrectWhenInputModelIsValidAndShouldBeAllowedOnlyByPostRequest()
            => MyController<CommentsApiController>
                .Instance()
                .WithUser()
                .WithData(data => data
                    .WithEntities(new Workout())
                    .WithEntities(new ApplicationUser
                    {
                        Id = "TestId",
                    }))
                .Calling(c => c.Add(new CommentInputModel
                {
                    Content = "blablablabla",
                    WorkoutId = 1,
                }))
                .ShouldHave()
                .ActionAttributes(attr => attr
                    .RestrictingForHttpMethod(HttpMethod.Post))
                .AndAlso()
                .ShouldReturn()
                .ResultOfType<CommentResponseModel>();

        [Fact]
        public void AddShouldReturnBadRequestAndShouldBeAllowedOnlyByPostRequest()
            => MyController<CommentsApiController>
                .Instance()
                .WithUser()
                .Calling(c => c.Add(With.Default<CommentInputModel>()))
                .ShouldHave()
                .ActionAttributes(attr => attr
                    .RestrictingForHttpMethod(HttpMethod.Post))
                .AndAlso()
                .ShouldReturn()
                .BadRequest();
    }
}
