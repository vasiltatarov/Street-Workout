namespace StreetWorkout.Test.Routing.Api
{
    using Xunit;
    using MyTested.AspNetCore.Mvc;
    using StreetWorkout.Controllers.Api;
    using ViewModels.Comments;

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
