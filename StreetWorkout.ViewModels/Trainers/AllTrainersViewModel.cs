namespace StreetWorkout.ViewModels.Trainers
{
    using System.Collections.Generic;

    public class AllTrainersViewModel
    {
        public const int TrainersPerPage = 9;

        public int CurrentPage { get; set; } = 1;

        public int TotalTrainers { get; set; }

        public IEnumerable<TrainerViewModel> Trainers { get; set; }
    }
}
