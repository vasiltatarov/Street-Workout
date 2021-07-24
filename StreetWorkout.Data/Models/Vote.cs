namespace StreetWorkout.Data.Models
{
    public class Vote
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public string VotedUserId { get; set; }

        public ApplicationUser VotedUser { get; set; }

        public byte Value { get; set; }
    }
}
