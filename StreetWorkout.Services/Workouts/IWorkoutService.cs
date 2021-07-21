namespace StreetWorkout.Services.Workouts
{
    using System.Collections.Generic;
    using StreetWorkout.ViewModels.Workouts;

    public interface IWorkoutService
    {
        WorkoutsQueryModel Workouts(string userId);

        IEnumerable<SportInCreateWorkoutModel> GetSports();

        IEnumerable<BodyPartInCreateWorkoutModel> GetBodyParts();

        bool IsValidSportId(int id);
    }
}
