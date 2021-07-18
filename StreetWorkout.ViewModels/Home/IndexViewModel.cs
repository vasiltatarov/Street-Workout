using System.Collections.Generic;

namespace StreetWorkout.ViewModels.Home
{
    public class IndexViewModel
    {
        public bool IsTrainer { get; set; }

        public bool IsAccountCompleted { get; set; }

        public List<UserIndexViewModel> Users { get; set; }
    }
}
