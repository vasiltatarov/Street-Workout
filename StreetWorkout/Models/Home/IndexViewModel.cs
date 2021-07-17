namespace StreetWorkout.Models.Home
{
    using System.Collections.Generic;

    public class IndexViewModel
    {
        public bool IsTrainer { get; set; }

        public bool IsAccountCompleted { get; set; }

        public List<UserIndexViewModel> Users { get; set; }
    }
}
