namespace StreetWorkout.Services.Homes
{
    using System.Collections.Generic;
    using Workouts;

    public class IndexServiceModel
    {
        public bool IsTrainer { get; set; }

        public bool IsAccountCompleted { get; set; }

        public List<UserIndexServiceModel> Users { get; set; }

        public IEnumerable<WorkoutServiceModel> Workouts { get; set; }
    }
}
