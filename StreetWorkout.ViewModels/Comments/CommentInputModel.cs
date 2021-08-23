namespace StreetWorkout.ViewModels.Comments
{
    using System.ComponentModel.DataAnnotations;

    using static ViewModelConstants;

    public class CommentInputModel
    {
        [Required]
        [StringLength(CommentInputModelContentMaxLength, MinimumLength = CommentInputModelContentMinLength)]
        public string Content { get; set; }

        public int WorkoutId { get; set; }
    }
}
