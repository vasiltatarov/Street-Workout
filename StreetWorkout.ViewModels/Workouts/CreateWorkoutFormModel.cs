namespace StreetWorkout.ViewModels.Workouts
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class CreateWorkoutFormModel
    {
        [Required]
        public string Title { get; set; }

        [Display(Name = "Difficult Level")]
        public int DifficultLevel { get; set; }

        [Range(5, 120)]
        public int Minutes { get; set; }

        [Display(Name = "Body Part Workout")]
        public int BodyPartId { get; set; }

        [Display(Name = "Sport")]
        public int SportId { get; set; }

        public IEnumerable<SportInCreateWorkoutModel> Sports { get; set; }

        public IEnumerable<BodyPartInCreateWorkoutModel> BodyParts { get; set; }
    }
}
