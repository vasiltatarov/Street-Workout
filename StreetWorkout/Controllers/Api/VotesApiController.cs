namespace StreetWorkout.Controllers.Api
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    using ViewModels.Votes;
    using Infrastructure;
    using Services.Votes;

    [Route("api/votes")]
    public class VotesApiController : ApiController
    {
        private readonly IVoteService votes;

        public VotesApiController(IVoteService votes)
            => this.votes = votes;

        [HttpPost]
        [Authorize]
        public ActionResult<VoteResponseModel> Vote(VoteInputModel vote)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest();
            }

            var votedUserId = this.User.GetId();

            if (votedUserId == vote.UserId)
            {
                return this.BadRequest();
            }

            this.votes.SetVote(vote.UserId, votedUserId, vote.Value);

            return this.votes.GetAverageVotes(vote.UserId);
        }
    }
}
