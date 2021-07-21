namespace StreetWorkout.Services.Workouts
{
    using System.Collections.Generic;
    using StreetWorkout.ViewModels.Workouts;
    using Data.Models.Enums;

    public interface IWorkoutService
    {
        void Create(string title, int sportId, DifficultLevel difficultLevel, int bodyPartId, string userId, int minutes, string content);

        WorkoutsQueryModel Workouts(string userId);

        IEnumerable<SportInCreateWorkoutModel> GetSports();

        IEnumerable<BodyPartInCreateWorkoutModel> GetBodyParts();

        bool IsValidSportId(int id);

        bool IsValidBodyPartId(int id);
    }
}
