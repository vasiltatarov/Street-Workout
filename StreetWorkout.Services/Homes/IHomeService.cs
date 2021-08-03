namespace StreetWorkout.Services.Homes
{
    using System.Collections.Generic;
    using Models;
    using StreetWorkout.Services.Workouts.Models;

    public interface IHomeService
    {
        IndexServiceModel IndexViewModel(string userId);

        IEnumerable<WorkoutServiceModel> Workouts();

        IEnumerable<UserIndexServiceModel> Users();
    }
}
