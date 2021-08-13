namespace StreetWorkout.Services.Homes
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Models;
    using StreetWorkout.Services.Workouts.Models;
    using StreetWorkout.Services.Supplements.Models;

    public interface IHomeService
    {
        Task<IndexServiceModel> IndexViewModel(string userId);

        Task<IEnumerable<WorkoutServiceModel>> Workouts();

        Task<IEnumerable<UserIndexServiceModel>> Users();

        Task<IEnumerable<SupplementServiceModel>> Supplements();
    }
}
