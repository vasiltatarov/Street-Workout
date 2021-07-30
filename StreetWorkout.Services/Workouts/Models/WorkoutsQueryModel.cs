namespace StreetWorkout.Services.Workouts.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class WorkoutsQueryModel
    {
        public const int WorkoutsPerPage = 9;

        public bool IsUserTrainer { get; set; }

        public int CurrentPage { get; set; } = 1;

        public int TotalWorkouts { get; set; }

        public string Sport { get; set; }

        [Display(Name = "Body Part")]
        public string BodyPart { get; set; }

        [Display(Name = "Search by text")]
        public string SearchTerms { get; set; }

        public IEnumerable<WorkoutServiceModel> Workouts { get; set; }

        public IEnumerable<string> Sports { get; set; }

        public IEnumerable<string> BodyParts { get; set; }
    }
}
