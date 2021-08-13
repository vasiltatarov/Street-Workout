namespace StreetWorkout.Services.Trainings
{
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;

    using Data;
    using ViewModels.Trainers;
    using Data.Models.Enums;

    public class TrainerService : ITrainerService
    {
        private readonly StreetWorkoutDbContext data;

        public TrainerService(StreetWorkoutDbContext data)
            => this.data = data;

        public async Task<AllTrainersViewModel> All(int currentPage)
            => new()
            {
                Trainers = await this.data
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
                    .ToListAsync(),
                TotalTrainers = await this.data
                    .UserDatas
                    .CountAsync(x => x.User.UserRole == UserRole.Trainer),
                CurrentPage = currentPage,
            };
    }
}
