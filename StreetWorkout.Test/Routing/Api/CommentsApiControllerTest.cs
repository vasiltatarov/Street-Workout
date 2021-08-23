namespace StreetWorkout.Test.Routing.Api
{
    using MyTested.AspNetCore.Mvc;
    using StreetWorkout.Controllers.Api;
    using StreetWorkout.ViewModels.Comments;
    using Xunit;

    public class CommentsApiControllerTest
    {
        [Fact]
        public void AddShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithPath("/api/comments")
                    .WithMethod(HttpMethod.Post))
                .To<CommentsApiController>(c => c.Add(With.Any<CommentInputModel>()));
    }
}
