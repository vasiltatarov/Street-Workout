namespace StreetWorkout.Services.GroupWorkouts
{
    using System.Collections.Generic;

    public class GroupWorkoutsQueryModel
    {
        public const int WorkoutsPerPage = 9;

        public bool IsUserTrainer { get; set; }

        public int CurrentPage { get; set; } = 1;

        public int TotalGroupWorkouts { get; set; }

        public IEnumerable<GroupWorkoutModel> GroupWorkouts { get; set; }
    }
}
