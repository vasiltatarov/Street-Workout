namespace StreetWorkout.ViewModels.Workouts
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static Common.DataConstants;

    public class WorkoutFormModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(WorkoutTitleMaxLength, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = WorkoutTitleMinLength)]
        public string Title { get; set; }

        [Display(Name = "Sport")]
        public int SportId { get; set; }

        [Display(Name = "Difficult Level")]
        public int DifficultLevel { get; set; }

        [Display(Name = "Body Part Workout")]
        public int BodyPartId { get; set; }

        [Range(CreateWorkoutFormModelMinutesMinValue, CreateWorkoutFormModelMinutesMaxValue)]
        public int Minutes { get; set; }

        [Required]
        public string Content { get; set; }

        public IEnumerable<SportViewModel> Sports { get; set; }

        public IEnumerable<BodyPartInCreateWorkoutViewModel> BodyParts { get; set; }
    }
}
