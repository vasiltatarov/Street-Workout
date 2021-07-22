namespace StreetWorkout.Services.Workouts
{
    using System.Collections.Generic;

    public class WorkoutsQueryModel
    {
        public const int WorkoutsPerPage = 9;

        public bool IsUserTrainer { get; set; }

        public int CurrentPage { get; set; } = 1;

        public int TotalWorkouts { get; set; }

        public IEnumerable<WorkoutServiceModel> Workouts { get; set; }
    }
}
