namespace StreetWorkout.Services.Votes
{
    using System.Linq;
    using Data;
    using Data.Models;
    using StreetWorkout.ViewModels.Votes;

    public class VoteService : IVoteService
    {
        private readonly StreetWorkoutDbContext data;

        public VoteService(StreetWorkoutDbContext data)
            => this.data = data;

        public void SetVote(string userId, string votedUserId, byte value)
        {
            var vote = this.data.Votes
                .FirstOrDefault(x => x.UserId == userId && x.VotedUserId == votedUserId);

            if (vote == null)
            {
                vote = new Vote
                {
                    UserId = userId,
                    VotedUserId = votedUserId,
                };
                this.data.Votes.Add(vote);
            }

            vote.Value = value;
            this.data.SaveChanges();
        }

        public VoteResponseModel GetAverageVotes(string userId)
            => new()
            {
                AverageVotes = this.data.Votes
                    .Any(x => x.UserId == userId)
                    ? this.data
                    .Votes
                    .Where(x => x.UserId == userId)
                    .Average(x => x.Value)
                    : 0
            };
    }
}
