namespace StreetWorkout.Services.Homes
{
    using System;
    using System.Linq;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using Data;
    using Models;
    using Data.Models.Enums;
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

        public IndexServiceModel IndexViewModel(string userId)
        {
            var user = this.data.Users.Find(userId);

            return new IndexServiceModel
            {
                IsAccountCompleted = user.IsAccountCompleted,
                IsTrainer = user.UserRole == UserRole.Trainer,
                Users = this.data
                    .UserDatas
                    .OrderByDescending(x => Guid.NewGuid())
                    .Take(3)
                    .ProjectTo<UserIndexServiceModel>(this.mapper.ConfigurationProvider)
                    .ToList(),
                Workouts = this.data
                    .Workouts
                    .OrderByDescending(x => Guid.NewGuid())
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
                    .ToList(),
            };
        }

        private static string GetImage(string bodyPart)
            => bodyPart switch
            {
                "Upper Body" => "https://cdna.artstation.com/p/assets/images/images/000/130/718/large/bhunesh-ramwani-uper-body.jpg?1405094263",
                "Lower Body" => "https://i.ytimg.com/vi/I9nG-G4B5Bs/maxresdefault.jpg",
                "Full Body" => "https://builtwithscience.com/wp-content/uploads/2019/01/full-body-workout-A-thumbnail-min.jpg",
                "Back" => "https://bestlifeonline.com/wp-content/uploads/2017/04/shutterstock_272601662-1024x682.jpg",
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
