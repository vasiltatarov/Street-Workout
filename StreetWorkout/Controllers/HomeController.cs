using System.Linq;
using Microsoft.AspNetCore.Authorization;
using StreetWorkout.Data;
using StreetWorkout.Data.Models;
using StreetWorkout.Infrastructure;
using StreetWorkout.Models.Home;

namespace StreetWorkout.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using System.Diagnostics;

    public class HomeController : Controller
    {
        private readonly StreetWorkoutDbContext data;

        public HomeController(StreetWorkoutDbContext data)
        {
            this.data = data;
        }

        [Authorize]
        public IActionResult Index()
        {
            var user = this.data.Users.Find(this.User.GetId());

            return View(new IndexViewModel
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
            });
        }

        public IActionResult Privacy()
            => View();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
            => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
