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
    using Services.Supplements.Models;
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
            const string SupplementsCacheKey = "SupplementsCacheKey";

            var latestWorkouts = this.cache.Get<IEnumerable<WorkoutServiceModel>>(LatestWorkoutsCacheKey);
            var users = this.cache.Get<IEnumerable<UserIndexServiceModel>>(UsersCacheKey);
            var latestSupplements = this.cache.Get<IEnumerable<SupplementServiceModel>>(SupplementsCacheKey);

            if (latestWorkouts == null || users == null || latestSupplements == null)
            {
                latestWorkouts = this.homeService.Workouts();
                users = this.homeService.Users();
                latestSupplements = this.homeService.Supplements();

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(10));

                this.cache.Set(LatestWorkoutsCacheKey, latestWorkouts, cacheOptions);
                this.cache.Set(UsersCacheKey, users, cacheOptions);
                this.cache.Set(SupplementsCacheKey, latestSupplements, cacheOptions);
            }

            var model = this.homeService.IndexViewModel(this.User.GetId());
            model.LatestWorkouts = latestWorkouts;
            model.Users = users;
            model.LatestSupplements = latestSupplements;

            return View(model);
        }

        public IActionResult Privacy()
            => View();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
            => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
