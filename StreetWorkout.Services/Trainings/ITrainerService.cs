namespace StreetWorkout.Services.Trainings
{
    using System.Threading.Tasks;
    using ViewModels.Trainers;

    public interface ITrainerService
    {
        Task<AllTrainersViewModel> All(int currentPage);
    }
}
