namespace StreetWorkout.Services.Accounts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using Data;
    using Data.Models;
    using Data.Models.Enums;
    using StreetWorkout.ViewModels.Accounts;
    using StreetWorkout.ViewModels.Workouts;

    public class AccountService : IAccountService
    {
        private readonly StreetWorkoutDbContext data;
        private readonly IMapper mapper;

        public AccountService(StreetWorkoutDbContext data, IMapper mapper)
        {
            this.data = data;
            this.mapper = mapper;
        }

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

        public AccountViewModel GetAccount(string username)
            => this.data.Users
                .Where(x => x.UserName == username)
                .Select(x => new AccountViewModel
                {
                    Id = x.Id,
                    IsTrainer = x.UserRole == UserRole.Trainer,
                    IsAccountComplete = x.IsAccountCompleted,
                    Username = x.UserName,
                    Age = DateTime.Now.Year - x.DateOfBirth.Year,
                    City = x.City,
                    Country = x.Country.Name,
                    ImageUrl = x.ImageUrl,
                    Gender = x.Gender.ToString(),
                    VotesAverageValue = this.data.Votes.Any(v => v.UserId == x.Id)
                        ? this.data.Votes.Where(v => v.UserId == x.Id).Average(v => v.Value)
                        : 0,
                    Data = !x.IsAccountCompleted
                        ? null
                        : this.data
                            .UserDatas
                            .Where(ud => ud.User.UserName == username)
                            .Select(ud => new AccountUserDataViewModel
                            {
                                Sport = ud.Sport.Name,
                                Goal = ud.Goal.Name,
                                TrainingFrequency = ud.TrainingFrequency.Name,
                                Weight = ud.Weight,
                                Height = ud.Height,
                                Description = ud.Description,
                            })
                            .FirstOrDefault(),
                })
                .FirstOrDefault();

        public IEnumerable<SportViewModel> GetSportsInAccountFormModel()
            => this.data.Sports
                .ProjectTo<SportViewModel>(this.mapper.ConfigurationProvider)
                .ToList();

        public IEnumerable<GoalInAccountViewModel> GetGoalsInAccountFormModel()
            => this.data.Goals
                .ProjectTo<GoalInAccountViewModel>(this.mapper.ConfigurationProvider)
                .ToList();

        public IEnumerable<TrainingFrequencyInAccountViewModel> GetTrainingFrequenciesInAccountFormModel()
            => this.data.TrainingFrequencies
                .Select(x => new TrainingFrequencyInAccountViewModel()
                {
                    Id = x.Id,
                    Name = x.Name
                })
                .ToList();
    }
}
