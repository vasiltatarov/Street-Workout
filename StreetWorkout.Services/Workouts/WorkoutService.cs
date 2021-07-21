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
            };

        public IEnumerable<SportInCreateWorkoutModel> GetSports()
            => this.data.Sports
                .Select(x => new SportInCreateWorkoutModel
                {
                    Id = x.Id,
                    Name = x.Name,
                })
                .ToList();

        public IEnumerable<BodyPartInCreateWorkoutModel> GetBodyParts()
            => this.data.BodyParts
                .Select(x => new BodyPartInCreateWorkoutModel
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
