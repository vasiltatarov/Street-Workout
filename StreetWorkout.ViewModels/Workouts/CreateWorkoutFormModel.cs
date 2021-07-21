namespace StreetWorkout.ViewModels.Workouts
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static Common.DataConstants;

    public class CreateWorkoutFormModel
    {
        [Required]
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

        public IEnumerable<SportInCreateWorkoutModel> Sports { get; set; }

        public IEnumerable<BodyPartInCreateWorkoutModel> BodyParts { get; set; }
    }
}
