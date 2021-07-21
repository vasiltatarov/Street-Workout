namespace StreetWorkout.Services.Trainings
{
    using System.Collections.Generic;
    using ViewModels.Trainers;

    public interface ITrainerService
    {
        IEnumerable<TrainerViewModel> All();
    }
}
