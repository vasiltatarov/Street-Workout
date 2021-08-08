namespace StreetWorkout.Test.Controllers.Api
{
    using MyTested.AspNetCore.Mvc;
    using Shouldly;
    using Xunit;

    using StreetWorkout.Controllers.Api;
    using ViewModels.Votes;

    public class VotesApiControllerTest
    {
        [Fact]
        public void VoteShouldReturnBadRequestWhenModelStateIsInvalidAndShouldBeAllowedOnlyByPostRequest()
            => MyController<VotesApiController>
                .Instance()
                .WithUser()
                .Calling(c => c.Vote(With.Default<VoteInputModel>()))
                .ShouldHave()
                .ActionAttributes(attr => attr
                    .RestrictingForHttpMethod(HttpMethod.Post))
                .AndAlso()
                .ShouldReturn()
                .BadRequest();

        [Fact]
        public void VoteBeAllowedOnlyByPostRequestAndShouldReturnBadRequestWhenUserTryToRateHimSelf()
            => MyController<VotesApiController>
                .Instance()
                .WithUser()
                .Calling(c => c.Vote(new VoteInputModel
                {
                    UserId = "TestId",
                    Value = 2,
                }))
                .ShouldHave()
                .ActionAttributes(attr => attr
                    .RestrictingForHttpMethod(HttpMethod.Post))
                .AndAlso()
                .ShouldReturn()
                .BadRequest();

        [Fact]
        public void VoteShouldBeAllowedOnlyByPostRequestAndShouldReturnCorrectVoteResponseModelWhenModelStateIsValid()
            => MyController<VotesApiController>
                .Instance()
                .WithUser()
                .Calling(c => c.Vote(new VoteInputModel
                {
                    UserId = "AnotherUserId",
                    Value = 2,
                }))
                .ShouldHave()
                .ActionAttributes(attr => attr
                    .RestrictingForHttpMethod(HttpMethod.Post))
                .AndAlso()
                .ShouldReturn()
                .ActionResult<VoteResponseModel>(result => result
                    .Passing(data =>
                    {
                        data.AverageVotes.ShouldBeGreaterThan(0);
                    }));
    }
}
