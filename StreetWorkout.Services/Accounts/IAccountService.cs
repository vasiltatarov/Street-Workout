﻿namespace StreetWorkout.Services.Accounts
{
    using System.Collections.Generic;
    using StreetWorkout.ViewModels.Accounts;
    using StreetWorkout.ViewModels.Workouts;
    public interface IAccountService
    {
        bool IsUserAccountComplete(string userId);

        bool IsUserDataExists(string userId);

        bool IsValidSportId(int id);

        bool IsValidGoalId(int id);

        bool IsValidTrainingFrequencyId(int id);

        void CompleteAccount(string userId, int sportId, int goalId, int trainingFrequency, int weight, int height, string description);

        AccountViewModel GetAccount(string username);

        IEnumerable<SportViewModel> GetSportsInAccountFormModel();

        IEnumerable<GoalInAccountViewModel> GetGoalsInAccountFormModel();

        IEnumerable<TrainingFrequencyInAccountViewModel> GetTrainingFrequenciesInAccountFormModel();
    }
}
