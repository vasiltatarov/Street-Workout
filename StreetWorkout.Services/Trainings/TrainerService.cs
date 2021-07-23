namespace StreetWorkout.Services.Trainings
{
    using System.Collections.Generic;
    using System.Linq;
    using Data;
    using Data.Models;
    using ViewModels.Trainers;

    public class TrainerService : ITrainerService
    {
        private readonly StreetWorkoutDbContext data;

        public TrainerService(StreetWorkoutDbContext data)
            => this.data = data;

        public IEnumerable<TrainerViewModel> All()
            => this.data
                .UserDatas
                .Where(x => x.User.UserRole == UserRole.Trainer)
                .Select(x => new TrainerViewModel
                {
                    Username = x.User.UserName,
                    ImageUrl = x.User.ImageUrl,
                    Sport = x.Sport.Name,
                    Goal = x.Goal.Name,
                    TrainingFrequency = x.TrainingFrequency.Name,
                })
                .ToList();
    }
}
