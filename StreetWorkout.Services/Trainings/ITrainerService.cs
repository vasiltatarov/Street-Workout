namespace StreetWorkout.Services.Trainings
{
    using ViewModels.Trainers;

    public interface ITrainerService
    {
        AllTrainersViewModel All(int currentPage);
    }
}
