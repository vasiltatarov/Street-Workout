namespace StreetWorkout.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using Services.Workouts;
    using Data.Models.Enums;
    using Infrastructure;
    using ViewModels.Workouts;
    using Services.Workouts.Models;

    using static WebConstants.TempDataMessageKeys;

    [Authorize]
    public class WorkoutsController : Controller
    {
        private readonly IWorkoutService workouts;

        public WorkoutsController(IWorkoutService workouts)
            => this.workouts = workouts;

        public async Task<IActionResult> All([FromQuery] WorkoutsQueryModel query)
        {
            var workoutsQuery = await this.workouts.All(this.User.GetId(), query.Sport, query.BodyPart, query.SearchTerms, query.CurrentPage);

            if (workoutsQuery.TotalWorkouts == 0)
            {
                this.TempData[NotFoundDataKey] = string.Format(NotFoundDataMessage, typeof(WorkoutsController).GetControllerName());
            }

            return this.View(workoutsQuery);
        }

        public async Task<IActionResult> Details(int id, string information)
        {
            var workout = await this.workouts.Details(id);

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

        public async Task<IActionResult> Create()
            => this.View(new WorkoutFormModel
            {
                Sports = await this.workouts.GetSports(),
                BodyParts = await this.workouts.GetBodyParts(),
            });

        [HttpPost]
        public async Task<IActionResult> Create(WorkoutFormModel workout)
        {
            if (!Enum.IsDefined(typeof(DifficultLevel), workout.DifficultLevel))
            {
                this.ModelState.AddModelError(nameof(workout.DifficultLevel), "Invalid difficult level.");
            }

            if (!await this.workouts.IsValidSportId(workout.SportId))
            {
                this.ModelState.AddModelError(nameof(workout.SportId), "Invalid sport.");
            }

            if (!await this.workouts.IsValidBodyPartId(workout.BodyPartId))
            {
                this.ModelState.AddModelError(nameof(workout.BodyPartId), "Invalid body part.");
            }

            if (!this.ModelState.IsValid)
            {
                workout.Sports = await this.workouts.GetSports();
                workout.BodyParts = await this.workouts.GetBodyParts();
                return this.View(workout);
            }

            await this.workouts.Create(workout.Title, workout.SportId, (DifficultLevel)workout.DifficultLevel, workout.BodyPartId, this.User.GetId(), workout.Minutes, workout.Content);

            return this.RedirectToAction("All");
        }
    }
}
