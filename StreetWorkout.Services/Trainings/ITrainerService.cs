namespace StreetWorkout.Services.Trainings
{
    using System.Threading.Tasks;
    using StreetWorkout.ViewModels.Trainers;

    public interface ITrainerService
    {
        Task<AllUsersQueryModel> All(int currentPag, string role, string sport);
    }
}
