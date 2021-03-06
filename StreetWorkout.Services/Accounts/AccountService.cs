namespace StreetWorkout.Services.Accounts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Microsoft.EntityFrameworkCore;

    using StreetWorkout.Data;
    using StreetWorkout.Data.Models;
    using StreetWorkout.Data.Models.Enums;
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

        public async Task<bool> IsUserAccountComplete(string userId)
        {
            var user = await this.data.Users.FindAsync(userId);

            return user?.IsAccountCompleted ?? false;
        }

        public async Task<bool> IsUserDataExists(string userId)
            => await this.data.UserDatas.AnyAsync(x => x.UserId == userId);

        public async Task<bool> IsValidSportId(int id)
            => await this.data.Sports.AnyAsync(x => x.Id == id);

        public async Task<bool> IsValidGoalId(int id)
            => await this.data.Goals.AnyAsync(x => x.Id == id);

        public async Task<bool> IsValidTrainingFrequencyId(int id)
            => await this.data.TrainingFrequencies.AnyAsync(x => x.Id == id);

        public async Task<bool> Edit(string userId, string imageUrl, int weight, int height, string city, string description, int sportId, int goalId, int trainingFrequencyId)
        {
            var user = await this.data.Users.FirstOrDefaultAsync(x => x.Id == userId);
            var userData = await this.data.UserDatas.FirstOrDefaultAsync(x => x.UserId == userId);

            if (userData == null || user == null)
            {
                return false;
            }

            user.ImageUrl = imageUrl;
            user.City = city;

            userData.SportId = sportId;
            userData.GoalId = goalId;
            userData.TrainingFrequencyId = trainingFrequencyId;
            userData.Weight = weight;
            userData.Height = height;
            userData.Description = description;

            await this.data.SaveChangesAsync();

            return true;
        }

        public async Task<bool> EditImage(string userId, string imageUrl)
        {
            var user = await this.data.Users.FirstOrDefaultAsync(x => x.Id == userId);

            if (user == null)
            {
                return false;
            }

            user.ImageUrl = imageUrl;

            await this.data.SaveChangesAsync();

            return true;
        }

        public async Task CompleteAccount(string userId, int sportId, int goalId, int trainingFrequency, int weight, int height, string description)
        {
            await this.data.UserDatas.AddAsync(new UserData
            {
                UserId = userId,
                SportId = sportId,
                GoalId = goalId,
                TrainingFrequencyId = trainingFrequency,
                Weight = weight,
                Height = height,
                Description = description,
            });

            var user = await this.data.Users.FindAsync(userId);
            user.IsAccountCompleted = true;

            await this.data.SaveChangesAsync();
        }

        public async Task<AccountViewModel> GetAccount(string username)
            => await this.data.Users
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
                .FirstOrDefaultAsync();

        public async Task<EditFormModel> GetEditFormModel(string userId)
            => await this.data
                .UserDatas
                .Where(x => x.UserId == userId)
                .Select(x => new EditFormModel
                {
                    Id = x.User.Id,
                    Username = x.User.UserName,
                    ImageUrl = x.User.ImageUrl,
                    Weight = x.Weight,
                    Height = x.Height,
                    Description = x.Description,
                    City = x.User.City,
                    SportId = x.SportId,
                    GoalId = x.GoalId,
                    TrainingFrequencyId = x.TrainingFrequencyId,
                })
                .FirstOrDefaultAsync();

        public async Task<EditImageFormModel> GetEditImageFormModel(string userId)
            => await this.data
                .Users
                .Where(x => x.Id == userId)
                .ProjectTo<EditImageFormModel>(this.mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

        public async Task<IEnumerable<SportViewModel>> GetSportsInAccountFormModel()
            => await this.data.Sports
                .ProjectTo<SportViewModel>(this.mapper.ConfigurationProvider)
                .ToListAsync();

        public async Task<IEnumerable<GoalInAccountViewModel>> GetGoalsInAccountFormModel()
            => await this.data.Goals
                .ProjectTo<GoalInAccountViewModel>(this.mapper.ConfigurationProvider)
                .ToListAsync();

        public async Task<IEnumerable<TrainingFrequencyInAccountViewModel>> GetTrainingFrequenciesInAccountFormModel()
            => await this.data.TrainingFrequencies
                .Select(x => new TrainingFrequencyInAccountViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                })
                .ToListAsync();
    }
}
