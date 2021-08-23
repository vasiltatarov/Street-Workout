namespace StreetWorkout.Test.Routing.Api
{
    using MyTested.AspNetCore.Mvc;
    using StreetWorkout.Controllers.Api;
    using StreetWorkout.ViewModels.Votes;
    using Xunit;

    public class VotesApiControllerTest
    {
        [Fact]
        public void VoteShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithPath("/api/votes")
                    .WithMethod(HttpMethod.Post))
                .To<VotesApiController>(c => c.Vote(With.Any<VoteInputModel>()));
    }
}
