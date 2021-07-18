namespace StreetWorkout.Models.Accounts
{
    using System.Collections.Generic;

    using StreetWorkout.Data.Models;

    public class CompleteAccountFormModel
    {
        public int Weight { get; set; }

        public int Height { get; set; }

        public int SportId { get; set; }

        public IEnumerable<Sport> Sports { get; set; }

        public string Goal { get; set; }

        public string Activity { get; set; }

        public string Description { get; set; }
    }
}
