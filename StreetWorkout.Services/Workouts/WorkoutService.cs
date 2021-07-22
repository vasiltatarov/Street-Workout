namespace StreetWorkout.Services.Workouts
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using StreetWorkout.ViewModels.Workouts;

    using Data;
    using Data.Models;
    using Data.Models.Enums;

    public class WorkoutService : IWorkoutService
    {
        private readonly StreetWorkoutDbContext data;

        public WorkoutService(StreetWorkoutDbContext data)
            => this.data = data;

        public void Create(string title, int sportId, DifficultLevel difficultLevel, int bodyPartId, string userId, int minutes, string content)
        {
            var workout = new Workout
            {
                Title = title,
                SportId = sportId,
                DifficultLevel = difficultLevel,
                BodyPartId = bodyPartId,
                UserId = userId,
                Minutes = minutes,
                Content = content,
                CreatedOn = DateTime.UtcNow,
            };
            this.data.Workouts.Add(workout);
            this.data.SaveChanges();
        }

        public WorkoutsQueryModel Workouts(string userId)
            => new()
            {
                IsUserTrainer = this.data.Users.Find(userId).UserRole == UserRole.Trainer,
                Workouts = this.data
                    .Workouts
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

        public WorkoutDetailsServiceModel Details(int id)
            => this.data
                .Workouts
                .Where(x => x.Id == id)
                .Select(x => new WorkoutDetailsServiceModel
                {
                    Title = x.Title,
                    Sport = x.Sport.Name,
                    DifficultLevel = x.DifficultLevel.ToString(),
                    BodyPart = x.BodyPart.Name,
                    ImageUrl = GetImage(x.BodyPart.Name),
                    UserUsername = x.User.UserName,
                    UserImageUrl = x.User.ImageUrl,
                    UserDescription = this.data
                        .UserDatas
                        .FirstOrDefault(ud => ud.UserId == x.UserId)
                        .Description,
                    Minutes = x.Minutes,
                    Content = x.Content,
                    CreatedOn = x.CreatedOn,
                })
                .FirstOrDefault();

        public IEnumerable<SportInCreateWorkoutViewModel> GetSports()
            => this.data.Sports
                .Select(x => new SportInCreateWorkoutViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                })
                .ToList();

        public IEnumerable<BodyPartInCreateWorkoutViewModel> GetBodyParts()
            => this.data.BodyParts
                .Select(x => new BodyPartInCreateWorkoutViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                })
                .ToList();

        public bool IsValidSportId(int id)
            => this.data.Sports.Any(x => x.Id == id);

        public bool IsValidBodyPartId(int id)
            => this.data.BodyParts.Any(x => x.Id == id);

        private static string GetImage(string bodyPart)
            => bodyPart switch
            {
                "Upper Body" => "https://cdna.artstation.com/p/assets/images/images/000/130/718/large/bhunesh-ramwani-uper-body.jpg?1405094263",
                "Back" => "https://bestlifeonline.com/wp-content/uploads/2017/04/shutterstock_272601662-1024x682.jpg",
                "Chest" => "https://i.ytimg.com/vi/lWXhih3xbVc/maxresdefault.jpg",
                _ => "",
            };
    }
}
