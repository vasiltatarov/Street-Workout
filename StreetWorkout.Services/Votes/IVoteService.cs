namespace StreetWorkout.Services.Votes
{
    using StreetWorkout.ViewModels.Votes;

    public interface IVoteService
    {
        void SetVote(string userId, string votedUserId, byte value);

        VoteResponseModel GetAverageVotes(string userId);
    }
}
