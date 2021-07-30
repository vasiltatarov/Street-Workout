namespace StreetWorkout.Services.Workouts
{
    using System.Collections.Generic;
    using StreetWorkout.ViewModels.Workouts;
    using Data.Models.Enums;
    using Models;

    public interface IWorkoutService
    {
        void Create(string title, int sportId, DifficultLevel difficultLevel, int bodyPartId, string userId, int minutes, string content);

        WorkoutsQueryModel Workouts(string userId, string sport, string bodyPart, string searchTerms, int currentPage);

        WorkoutDetailsServiceModel Details(int id);

        IEnumerable<SportViewModel> GetSports();

        IEnumerable<BodyPartInCreateWorkoutViewModel> GetBodyParts();

        bool IsValidSportId(int id);

        bool IsValidBodyPartId(int id);
    }
}
