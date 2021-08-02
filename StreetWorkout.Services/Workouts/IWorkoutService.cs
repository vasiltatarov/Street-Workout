namespace StreetWorkout.Services.Workouts
{
    using System.Collections.Generic;
    using StreetWorkout.ViewModels.Workouts;
    using Data.Models.Enums;
    using Models;

    public interface IWorkoutService
    {
        void Create(string title, int sportId, DifficultLevel difficultLevel, int bodyPartId, string userId, int minutes, string content);

        bool Edit(int id, string title, int sportId, DifficultLevel difficultLevel, int bodyPartId, int minutes, string content);

        WorkoutsQueryModel Workouts(string userId, string sport, string bodyPart, string searchTerms, int currentPage);

        WorkoutDetailsServiceModel Details(int id);

        public WorkoutFormModel EditFormModel(int id);

        IEnumerable<SportViewModel> GetSports();

        IEnumerable<BodyPartInCreateWorkoutViewModel> GetBodyParts();

        bool IsUserCreator(string userId, int workoutId);

        bool IsValidSportId(int id);

        bool IsValidBodyPartId(int id);

        bool Delete(int id);
    }
}
