namespace StreetWorkout.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Memory;
    using StreetWorkout.Infrastructure;
    using StreetWorkout.Services.Homes;
    using StreetWorkout.Services.Homes.Models;
    using StreetWorkout.Services.Supplements.Models;
    using StreetWorkout.Services.Workouts.Models;
    using StreetWorkout.ViewModels;

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
        public async Task<IActionResult> Index()
        {
            const string LatestWorkoutsCacheKey = "WorkoutsKey";
            const string UsersCacheKey = "UsersKey";
            const string SupplementsCacheKey = "SupplementsCacheKey";

            var latestWorkouts = this.cache.Get<IEnumerable<WorkoutServiceModel>>(LatestWorkoutsCacheKey);
            var users = this.cache.Get<IEnumerable<UserIndexServiceModel>>(UsersCacheKey);
            var latestSupplements = this.cache.Get<IEnumerable<SupplementServiceModel>>(SupplementsCacheKey);

            if (latestWorkouts == null || users == null || latestSupplements == null)
            {
                latestWorkouts = await this.homeService.Workouts();
                users = await this.homeService.Users();
                latestSupplements = await this.homeService.Supplements();

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(10));

                this.cache.Set(LatestWorkoutsCacheKey, latestWorkouts, cacheOptions);
                this.cache.Set(UsersCacheKey, users, cacheOptions);
                this.cache.Set(SupplementsCacheKey, latestSupplements, cacheOptions);
            }

            var model = await this.homeService.IndexViewModel(this.User.GetId());
            model.LatestWorkouts = latestWorkouts;
            model.Users = users;
            model.LatestSupplements = latestSupplements;

            return this.View(model);
        }

        public IActionResult Privacy()
            => this.View();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
            => this.View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
    }
}
