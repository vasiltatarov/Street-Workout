using System.Linq;
using StreetWorkout.Data.Models;
using StreetWorkout.Infrastructure;

namespace StreetWorkout.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using StreetWorkout.Data;

    [Authorize]
    public class AccountsController : Controller
    {
        private readonly StreetWorkoutDbContext data;

        public AccountsController(StreetWorkoutDbContext data)
        {
            this.data = data;
        }

        public IActionResult CompleteAccount()
        {
            return this.View();
        }

        //[HttpPost]
        //public IActionResult CompleteAccount(UserDataFormModel data)
        //{

        //}
    }
}
