namespace StreetWorkout.ViewModels.Trainers
{
    using System.Collections.Generic;
    using Workouts;

    public class AllUsersQueryModel
    {
        public const int TrainersPerPage = 9;

        public int CurrentPage { get; set; } = 1;

        public int TotalUsers { get; set; }

        public string Role { get; set; }

        public string Sport { get; set; }

        public IEnumerable<UserViewModel> Users { get; set; }

        public IEnumerable<SportViewModel> Sports { get; set; }
    }
}
