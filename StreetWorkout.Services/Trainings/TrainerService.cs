namespace StreetWorkout.Services.Trainings
{
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using StreetWorkout.Data;
    using StreetWorkout.Data.Models.Enums;
    using StreetWorkout.Services.Workouts;
    using StreetWorkout.ViewModels.Trainers;

    public class TrainerService : ITrainerService
    {
        private readonly StreetWorkoutDbContext data;
        private readonly IWorkoutService workouts;

        public TrainerService(StreetWorkoutDbContext data, IWorkoutService workouts)
        {
            this.data = data;
            this.workouts = workouts;
        }

        public async Task<AllUsersQueryModel> All(int currentPage, string role, string sport)
        {
            var usersQuery = this.data
                .Users
                .OrderByDescending(x => x.CreatedOn)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(role))
            {
                if (role == "trainers")
                {
                    usersQuery = usersQuery
                        .Where(x => x.UserRole == UserRole.Trainer)
                        .AsQueryable();
                }
                else if (role == "enthusiasts")
                {
                    usersQuery = usersQuery
                        .Where(x => x.UserRole == UserRole.Enthusiast)
                        .AsQueryable();
                }
            }

            if (!string.IsNullOrWhiteSpace(sport))
            {
                usersQuery = usersQuery
                    .Where(x => this.data.UserDatas
                        .Any(ud => ud.UserId == x.Id &&
                                   ud.Sport.Name.ToLower() == sport.ToLower()));
            }

            var users = await usersQuery
                .Skip((currentPage - 1) * AllUsersQueryModel.TrainersPerPage)
                .Take(AllUsersQueryModel.TrainersPerPage)
                .Select(x => new UserViewModel()
                {
                    IsTrainer = x.UserRole == UserRole.Trainer,
                    Username = x.UserName,
                    ImageUrl = x.ImageUrl,
                    Sport = this.data
                        .UserDatas
                        .Any(ud => ud.UserId == x.Id)
                        ? this.data.UserDatas.FirstOrDefault(ud => ud.UserId == x.Id).Sport.Name
                        : "Missing",
                    VotesAverageValue = this.data
                        .Votes
                        .Any(v => v.UserId == x.Id)
                        ? this.data
                            .Votes
                            .Where(v => v.UserId == x.Id)
                            .Average(v => v.Value)
                        : 0,
                })
                .ToListAsync();

            var totalUsers = usersQuery.Count();

            var sports = await this.workouts.GetSports();

            return new AllUsersQueryModel
            {
                Users = users,
                TotalUsers = totalUsers,
                CurrentPage = currentPage,
                Role = role,
                Sport = sport,
                Sports = sports,
            };
        }
    }
}
