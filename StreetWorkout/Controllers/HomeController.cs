namespace StreetWorkout.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.Extensions.Caching.Memory;

    using Services.Homes;
    using Services.Homes.Models;
    using Services.Workouts.Models;
    using ViewModels;
    using Infrastructure;

    public class HomeController : Controller
    {
        private readonly IHomeService homeService;
        private readonly IMemoryCache cache;

        public HomeController(IHomeService homeService, IMemoryCache cache)
        {
            this.homeService = homeService;
            this.cache = cache;
        }

        [Authorize]
        public IActionResult Index()
        {
            const string LatestWorkoutsCacheKey = "WorkoutsKey";
            const string UsersCacheKey = "UsersKey";

            var latestWorkouts = this.cache.Get<IEnumerable<WorkoutServiceModel>>(LatestWorkoutsCacheKey);
            var users = this.cache.Get<IEnumerable<UserIndexServiceModel>>(UsersCacheKey);

            if (latestWorkouts == null || users == null)
            {
                latestWorkouts = this.homeService.Workouts();
                users = this.homeService.Users();

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(10));

                this.cache.Set(LatestWorkoutsCacheKey, latestWorkouts, cacheOptions);
                this.cache.Set(UsersCacheKey, users, cacheOptions);
            }

            var model = this.homeService.IndexViewModel(this.User.GetId());
            model.LatestWorkouts = latestWorkouts;
            model.Users = users;

            return View(model);
        }

        public IActionResult Privacy()
            => View();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
            => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
