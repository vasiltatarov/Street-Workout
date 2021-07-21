namespace StreetWorkout.Services.Homes
{
    using System.Linq;
    using Data;
    using Data.Models;
    using ViewModels.Home;

    public class HomeService : IHomeService
    {
        private readonly StreetWorkoutDbContext data;

        public HomeService(StreetWorkoutDbContext data)
            => this.data = data;

        public IndexViewModel IndexViewModel(string userId)
        {
            var user = this.data.Users.Find(userId);

            return new IndexViewModel
            {
                IsAccountCompleted = user.IsAccountCompleted,
                IsTrainer = user.UserRole == UserRole.Trainer,
                Users = this.data.Users
                    //.Where(x => x.UserRole == UserRole.Trainer)
                    .Select(x => new UserIndexViewModel
                    {
                        Username = x.UserName,
                        ImageUrl = x.ImageUrl,
                    })
                    .ToList(),
            };
        }
    }
}
