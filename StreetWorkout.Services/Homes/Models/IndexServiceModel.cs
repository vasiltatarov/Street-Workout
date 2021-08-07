using StreetWorkout.Services.Supplements.Models;

namespace StreetWorkout.Services.Homes.Models
{
    using System.Collections.Generic;
    using StreetWorkout.Services.Workouts.Models;

    public class IndexServiceModel
    {
        public bool IsTrainer { get; set; }

        public bool IsAccountCompleted { get; set; }

        public IEnumerable<UserIndexServiceModel> Users { get; set; }

        public IEnumerable<WorkoutServiceModel> LatestWorkouts { get; set; }

        public IEnumerable<SupplementServiceModel> LatestSupplements { get; set; }
    }
}
