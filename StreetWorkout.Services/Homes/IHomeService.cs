namespace StreetWorkout.Services.Homes
{
    using System.Collections.Generic;
    using Models;
    using StreetWorkout.Services.Workouts.Models;
    using StreetWorkout.Services.Supplements.Models;

    public interface IHomeService
    {
        IndexServiceModel IndexViewModel(string userId);

        IEnumerable<WorkoutServiceModel> Workouts();

        IEnumerable<UserIndexServiceModel> Users();

        IEnumerable<SupplementServiceModel> Supplements();
    }
}
