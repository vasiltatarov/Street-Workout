namespace StreetWorkout.Services.Trainings
{
    using System.Linq;
    using Data;
    using Data.Models;
    using ViewModels.Trainers;

    public class TrainerService : ITrainerService
    {
        private readonly StreetWorkoutDbContext data;

        public TrainerService(StreetWorkoutDbContext data)
            => this.data = data;

        public AllTrainersViewModel All(int currentPage)
            => new()
            {
                Trainers = this.data
                    .UserDatas
                    .Where(x => x.User.UserRole == UserRole.Trainer)
                    .Skip((currentPage - 1) * AllTrainersViewModel.TrainersPerPage)
                    .Take(AllTrainersViewModel.TrainersPerPage)
                    .Select(x => new TrainerViewModel
                    {
                        Username = x.User.UserName,
                        ImageUrl = x.User.ImageUrl,
                        Sport = x.Sport.Name,
                        Goal = x.Goal.Name,
                        VotesAverageValue = this.data
                            .Votes
                            .Any(v => v.UserId == x.UserId)
                            ? this.data
                            .Votes
                            .Where(v => v.UserId == x.UserId)
                            .Average(v => v.Value)
                            : 0,
                    })
                    .ToList(),
                TotalTrainers = this.data
                    .UserDatas
                    .Count(x => x.User.UserRole == UserRole.Trainer),
                CurrentPage = currentPage,
            };
    }
}
