namespace StreetWorkout.Controllers
{
    using System;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using Services.Workouts;
    using Data.Models.Enums;
    using Infrastructure;
    using ViewModels.Workouts;

    [Authorize]
    public class WorkoutsController : Controller
    {
        private readonly IWorkoutService workouts;

        public WorkoutsController(IWorkoutService workouts)
            => this.workouts = workouts;

        public IActionResult All()
            => this.View(this.workouts.Workouts(this.User.GetId()));

        public IActionResult Create()
            => this.View(new CreateWorkoutFormModel
            {
                Sports = this.workouts.GetSports(),
                BodyParts = this.workouts.GetBodyParts(),
            });

        [HttpPost]
        public IActionResult Create(CreateWorkoutFormModel workout)
        {
            if (!Enum.IsDefined(typeof(DifficultLevel), workout.DifficultLevel))
            {
                this.ModelState.AddModelError(nameof(workout.DifficultLevel), "Invalid Difficult Level");
            }

            if (!this.workouts.IsValidSportId(workout.SportId))
            {
                this.ModelState.AddModelError(nameof(workout.SportId), "Invalid Sport");
            }

            if (!this.ModelState.IsValid)
            {
                workout.Sports = this.workouts.GetSports();
                workout.BodyParts = this.workouts.GetBodyParts();
                return this.View(workout);
            }

            return this.RedirectToAction("All");
        }
    }
}
