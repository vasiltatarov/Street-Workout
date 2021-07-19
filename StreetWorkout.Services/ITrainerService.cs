namespace StreetWorkout.Services
{
    using System.Collections.Generic;

    using ViewModels.Trainers;

    public interface ITrainerService
    {
        IEnumerable<TrainerViewModel> All();
    }
}
