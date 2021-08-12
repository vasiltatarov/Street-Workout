namespace StreetWorkout.Controllers
{
    using System;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using Services.Workouts;
    using Data.Models.Enums;
    using Infrastructure;
    using ViewModels.Workouts;
    using Services.Workouts.Models;

    [Authorize]
    public class WorkoutsController : Controller
    {
        private readonly IWorkoutService workouts;

        public WorkoutsController(IWorkoutService workouts)
            => this.workouts = workouts;

        public IActionResult All([FromQuery]WorkoutsQueryModel query)
            => this.View(this.workouts.Workouts(this.User.GetId(), query.Sport, query.BodyPart, query.SearchTerms, query.CurrentPage));

        public IActionResult Details(int id, string information)
        {
            var workout = this.workouts.Details(id);

            if (workout == null)
            {
                return this.BadRequest();
            }

            var informationDecode = System.Web.HttpUtility.UrlDecode(information);

            if (!informationDecode.Contains(workout.Title) &&
                !information.Contains(workout.Title) &&
                !informationDecode.Contains(workout.Sport))
            {
                return this.BadRequest();
            }

            return this.View(workout);
        }

        public IActionResult Create()
            => this.View(new WorkoutFormModel
            {
                Sports = this.workouts.GetSports(),
                BodyParts = this.workouts.GetBodyParts(),
            });

        [HttpPost]
        public IActionResult Create(WorkoutFormModel workout)
        {
            if (!Enum.IsDefined(typeof(DifficultLevel), workout.DifficultLevel))
            {
                this.ModelState.AddModelError(nameof(workout.DifficultLevel), "Invalid difficult level.");
            }

            if (!this.workouts.IsValidSportId(workout.SportId))
            {
                this.ModelState.AddModelError(nameof(workout.SportId), "Invalid sport.");
            }

            if (!this.workouts.IsValidBodyPartId(workout.BodyPartId))
            {
                this.ModelState.AddModelError(nameof(workout.BodyPartId), "Invalid body part.");
            }

            if (!this.ModelState.IsValid)
            {
                workout.Sports = this.workouts.GetSports();
                workout.BodyParts = this.workouts.GetBodyParts();
                return this.View(workout);
            }

            this.workouts.Create(workout.Title, workout.SportId, (DifficultLevel)workout.DifficultLevel, workout.BodyPartId, this.User.GetId(), workout.Minutes, workout.Content);

            return this.RedirectToAction("All");
        }
    }
}
