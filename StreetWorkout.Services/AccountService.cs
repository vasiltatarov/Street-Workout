﻿namespace StreetWorkout.Services
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    
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

        public AccountViewModel GetAccount(string username)
            => this.data.Users
                .Where(x => x.UserName == username)
                .Select(x => new AccountViewModel
                {
                    IsTrainer = x.UserRole == UserRole.Trainer,
                    IsAccountComplete = x.IsAccountCompleted,
                    Username = x.UserName,
                    Age = DateTime.Now.Year - x.DateOfBirth.Year,
                    City = x.City,
                    Country = x.Country.Name,
                    ImageUrl = x.ImageUrl,
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

        public IEnumerable<SportInAccountViewModel> GetSportsInAccountFormModel()
            => this.data.Sports
                .Select(x => new SportInAccountViewModel
                {
                    Id = x.Id,
                    Name = x.Name
                })
                .ToList();

        public IEnumerable<GoalInAccountViewModel> GetGoalsInAccountFormModel()
            => this.data.Goals
                .Select(x => new GoalInAccountViewModel
                {
                    Id = x.Id,
                    Name = x.Name
                })
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