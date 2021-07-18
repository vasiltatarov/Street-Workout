namespace StreetWorkout.Services
{
    using System.Collections.Generic;
    using System.Linq;
    
    using Data;
    using Data.Models;
    using ViewModels.Accounts;

    public class AccountService : IAccountService
    {
        private readonly StreetWorkoutDbContext data;

        public AccountService(StreetWorkoutDbContext data)
            => this.data = data;

        public bool IsUserAccountComplete(string userId)
            => this.data.Users.Find(userId).IsAccountCompleted;

        public bool IsUserDataExists(string userId)
            => this.data.UserDatas.Any(x => x.UserId == userId);

        public bool IsValidSportId(int id)
            => this.data.Sports.Any(x => x.Id == id);

        public bool IsValidGoalId(int id)
            => this.data.Goals.Any(x => x.Id == id);

        public bool IsValidTrainingFrequencyId(int id)
            => this.data.TrainingFrequencies.Any(x => x.Id == id);

        public void CompleteAccount(string userId, int sportId, int goalId, int trainingFrequency, int weight, int height,
            string description)
        {
            var userData = new UserData
            {
                UserId = userId,
                SportId = sportId,
                GoalId = goalId,
                TrainingFrequencyId = trainingFrequency,
                Weight = weight,
                Height = height,
                Description = description,
            };

            this.data.UserDatas.Add(userData);

            var user = this.data.Users.Find(userId);
            user.IsAccountCompleted = true;

            this.data.SaveChanges();
        }

        public IEnumerable<SportInAccountViewModel> GetSportsInAccountFormModel()
            => this.data.Sports
                .Select(x => new SportInAccountViewModel
                {
                    Id = x.Id,
                    Name = x.Name
                })
                .ToList();

        public IEnumerable<GoalInAccountViewModel> GetGoalsInAccountFormModel()
            => this.data.Sports
                .Select(x => new GoalInAccountViewModel
                {
                    Id = x.Id,
                    Name = x.Name
                })
                .ToList();

        public IEnumerable<TrainingFrequencyInAccountViewModel> GetTrainingFrequenciesInAccountFormModel()
            => this.data.Sports
                .Select(x => new TrainingFrequencyInAccountViewModel()
                {
                    Id = x.Id,
                    Name = x.Name
                })
                .ToList();
    }
}
