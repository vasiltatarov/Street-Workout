namespace StreetWorkout.Services.Homes.Models
{
    using System.Collections.Generic;
    using StreetWorkout.Services.Workouts.Models;

    public class IndexServiceModel
    {
        public bool IsTrainer { get; set; }

        public bool IsAccountCompleted { get; set; }

        public List<UserIndexServiceModel> Users { get; set; }

        public IEnumerable<WorkoutServiceModel> Workouts { get; set; }
    }
}
