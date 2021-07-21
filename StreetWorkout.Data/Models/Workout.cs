namespace StreetWorkout.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using Enums;

    using static DataConstants;

    public class Workout
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(WorkoutTitleMaxLength)]
        public string Title { get; set; }

        public int SportId { get; set; }

        public Sport Sport { get; set; }

        public DifficultLevel DifficultLevel { get; set; }

        public int BodyPartId { get; set; }

        public BodyPart BodyPart { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        [MaxLength(CreateWorkoutFormModelMinutesMaxValue)]
        public int Minutes { get; set; }

        [Required]
        public string Content { get; set; }
    }
}
