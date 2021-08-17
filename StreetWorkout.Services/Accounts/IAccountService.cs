namespace StreetWorkout.Services.Accounts
{
    using System.Threading.Tasks;
    using System.Collections.Generic;
    
    using StreetWorkout.ViewModels.Accounts;
    using StreetWorkout.ViewModels.Workouts;

    public interface IAccountService
    {
        Task<bool> IsUserAccountComplete(string userId);

        Task<bool> IsUserDataExists(string userId);

        Task<bool> IsValidSportId(int id);

        Task<bool> IsValidGoalId(int id);

        Task<bool> IsValidTrainingFrequencyId(int id);

        Task<bool> Edit(string userId, string imageUrl, int weight, int height, string city, string description, int sportId, int goalId, int trainingFrequencyId);

        Task<bool> EditImage(string userId, string imageUrl);

        Task CompleteAccount(string userId, int sportId, int goalId, int trainingFrequency, int weight, int height, string description);

        Task<AccountViewModel> GetAccount(string username);

        Task<EditFormModel> GetEditFormModel(string userId);

        Task<EditImageFormModel> GetEditImageFormModel(string userId);

        Task<IEnumerable<SportViewModel>> GetSportsInAccountFormModel();

        Task<IEnumerable<GoalInAccountViewModel>> GetGoalsInAccountFormModel();

        Task<IEnumerable<TrainingFrequencyInAccountViewModel>> GetTrainingFrequenciesInAccountFormModel();
    }
}
