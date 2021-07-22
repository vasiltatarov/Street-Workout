namespace StreetWorkout.Services.Workouts
{
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
                        Title = x.Title,
                        Sport = x.Sport.Name,
                        DifficultLevel = x.DifficultLevel.ToString(),
                        BodyPart = x.BodyPart.Name,
                        TrainerUsername = x.User.UserName,
                        TrainerImageUrl = x.User.ImageUrl,
                        Minutes = x.Minutes,
                        Content = x.Content,
                    })
                    .ToList(),
            };

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
    }
}
