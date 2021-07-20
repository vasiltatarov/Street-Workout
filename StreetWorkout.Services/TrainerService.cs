namespace StreetWorkout.Services
{
    using System.Linq;
    using System.Collections.Generic;

    using Data;
    using Data.Models;
    using ViewModels.Trainers;

    public class TrainerService : ITrainerService
    {
        private readonly StreetWorkoutDbContext data;

        public TrainerService(StreetWorkoutDbContext data)
            => this.data = data;

        public IEnumerable<TrainerViewModel> All()
            => this.data.Users
                .Where(x => x.UserRole == UserRole.Trainer)
                .Select(x => new TrainerViewModel
                {
                    Username = x.UserName,
                    ImageUrl = x.ImageUrl,
                })
                .ToList();
    }
}
