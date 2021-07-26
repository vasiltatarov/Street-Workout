namespace StreetWorkout.Controllers
{
    using System;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using Infrastructure;
    using Services.Workouts;
    using ViewModels.GroupWorkouts;

    [Authorize]
    public class GroupWorkoutsController : Controller
    {
        private readonly IWorkoutService workouts;

        public GroupWorkoutsController(IWorkoutService workouts)
            => this.workouts = workouts;

        public IActionResult All()
            => this.View();

        public IActionResult Create()
            => this.View(new GroupWorkoutFormModel
            {
                Sports = this.workouts.GetSports(),
            });

        [HttpPost]
        public IActionResult Create(GroupWorkoutFormModel input)
        {
            if (!this.workouts.IsValidSportId(input.SportId))
            {
                this.ModelState.AddModelError(nameof(input.SportId), "Sport is invalid.");
            }

            if (input.StartOn <= DateTime.UtcNow || input.EndOn <= input.StartOn)
            {
                this.ModelState.AddModelError(nameof(input.StartOn), "Start Workout Time must be bigger than now. Also End Time must be bigger than the Start Time");
            }

            if (input.EndOn <= DateTime.UtcNow || input.EndOn <= input.StartOn)
            {
                this.ModelState.AddModelError(nameof(input.EndOn), "End Workout Time must be bigger than now. Also End Time must be bigger than the Start Time");
            }

            if (!this.ModelState.IsValid)
            {
                input.Sports = this.workouts.GetSports();
                return this.View(input);
            }

            var userId = this.User.GetId();
            // add 

            return this.RedirectToAction("All");
        }
    }
}
