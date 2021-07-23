namespace StreetWorkout.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants;

    public class Comment
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(CommentContentMaxLength)]
        public string Content { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public int WorkoutId { get; set; }

        public Workout Workout { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
