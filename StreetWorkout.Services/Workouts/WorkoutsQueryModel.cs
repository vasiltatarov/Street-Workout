namespace StreetWorkout.Services.Workouts
{
    using System.Collections.Generic;

    public class WorkoutsQueryModel
    {
        public bool IsUserTrainer { get; set; }

        public IEnumerable<WorkoutServiceModel> Workouts { get; set; }
    }
}
