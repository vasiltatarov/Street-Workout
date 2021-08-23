namespace StreetWorkout.Services.Workouts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using StreetWorkout.Data.Models.Enums;
    using StreetWorkout.Services.Workouts.Models;
    using StreetWorkout.ViewModels.Workouts;

    public interface IWorkoutService
    {
        Task Create(string title, int sportId, DifficultLevel difficultLevel, int bodyPartId, string userId, int minutes, string content);

        Task<bool> IsValidSportId(int id);

        Task<bool> IsValidBodyPartId(int id);

        Task<bool> Delete(int id);

        Task<bool> Edit(int id, string title, int sportId, DifficultLevel difficultLevel, int bodyPartId, int minutes, string content);

        Task<bool> IsUserCreator(string userId, int workoutId);

        Task<WorkoutsQueryModel> All(string userId, string sport, string bodyPart, string searchTerms, int currentPage);

        Task<WorkoutDetailsServiceModel> Details(int id);

        Task<WorkoutFormModel> EditFormModel(int id);

        Task<IEnumerable<SportViewModel>> GetSports();

        Task<IEnumerable<BodyPartViewModel>> GetBodyParts();
    }
}
