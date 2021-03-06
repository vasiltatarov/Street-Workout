namespace StreetWorkout.Services.Homes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Microsoft.EntityFrameworkCore;
    using StreetWorkout.Data;
    using StreetWorkout.Data.Models.Enums;
    using StreetWorkout.Services.Homes.Models;
    using StreetWorkout.Services.Supplements.Models;
    using StreetWorkout.Services.Workouts.Models;

    public class HomeService : IHomeService
    {
        private readonly StreetWorkoutDbContext data;
        private readonly IMapper mapper;

        public HomeService(StreetWorkoutDbContext data, IMapper mapper)
        {
            this.data = data;
            this.mapper = mapper;
        }

        public async Task<IndexServiceModel> IndexViewModel(string userId)
        {
            var user = await this.data.Users.FindAsync(userId);

            return new IndexServiceModel
            {
                IsAccountCompleted = user.IsAccountCompleted,
                IsTrainer = user.UserRole == UserRole.Trainer,
            };
        }

        public async Task<IEnumerable<WorkoutServiceModel>> Workouts()
            => await this.data
                .Workouts
                .OrderByDescending(x => x.Id)
                .Take(3)
                .Select(x => new WorkoutServiceModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Sport = x.Sport.Name,
                    DifficultLevel = x.DifficultLevel.ToString(),
                    BodyPart = x.BodyPart.Name,
                    ImageUrl = GetImage(x.BodyPart.Name),
                    Minutes = x.Minutes,
                })
                .ToListAsync();

        public async Task<IEnumerable<UserIndexServiceModel>> Users()
            => await this.data
                .Users
                .OrderByDescending(x => x.CreatedOn)
                .Take(3)
                .ProjectTo<UserIndexServiceModel>(this.mapper.ConfigurationProvider)
                .ToListAsync();

        public async Task<IEnumerable<SupplementServiceModel>> Supplements()
            => await this.data
                .Supplements
                .OrderByDescending(x => x.Id)
                .Take(3)
                .ProjectTo<SupplementServiceModel>(this.mapper.ConfigurationProvider)
                .ToListAsync();

        private static string GetImage(string bodyPart)
            => bodyPart switch
            {
                "Upper Body" => "https://cdna.artstation.com/p/assets/images/images/000/130/718/large/bhunesh-ramwani-uper-body.jpg?1405094263",
                "Lower Body" => "https://i.ytimg.com/vi/I9nG-G4B5Bs/maxresdefault.jpg",
                "Full Body" => "https://builtwithscience.com/wp-content/uploads/2019/01/full-body-workout-A-thumbnail-min.jpg",
                "Back" => "https://musclemaker.com.au/wp-content/uploads/2020/03/shutterstock_269153795.jpg",
                "Chest" => "https://i.ytimg.com/vi/lWXhih3xbVc/maxresdefault.jpg",
                "Forearms" => "https://i.ytimg.com/vi/0XS0j1Gtobw/maxresdefault.jpg",
                "Triceps" => "https://builtwithscience.com/wp-content/uploads/2018/08/best-tricep-exercises.png",
                "Arms" => "https://bonytobeastly.com/wp-content/uploads/2021/05/arm-muscle-anatomy-for-bodybuilding-1.jpg",
                "Biceps" => "https://athleanx.com/wp-content/uploads/2018/04/biceps-muscles-anatomy-brachialis-long-head-short-head.jpg",
                "Shoulders" => "https://i.ytimg.com/vi/2Vprklw8cu8/maxresdefault.jpg",
                "Legs" => "https://i.ytimg.com/vi/RjexvOAsVtI/hqdefault.jpg",
                "ABS" => "https://i.pinimg.com/originals/63/37/aa/6337aa509ce4faa748ba48f383e67a80.jpg",
                "Neck" => "https://i.pinimg.com/originals/27/e2/c4/27e2c4879437c4420b9b8dc4ad991d91.jpg",
                _ => "https://i.pinimg.com/originals/0c/4b/34/0c4b3494a30b15368539140920cbf2ed.png",
            };
    }
}
