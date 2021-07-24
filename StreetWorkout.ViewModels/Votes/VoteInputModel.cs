namespace StreetWorkout.ViewModels.Votes
{
    using System.ComponentModel.DataAnnotations;

    public class VoteInputModel
    {
        public string UserId { get; set; }

        [Range(1, 5)]
        public byte Value { get; set; }
    }
}
