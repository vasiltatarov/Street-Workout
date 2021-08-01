namespace StreetWorkout.Areas.Administration.Controllers
{
    using System;
    using Microsoft.AspNetCore.Mvc;
    using Services.Workouts;
    using ViewModels.Workouts;
    using Data.Models.Enums;

    public class WorkoutsController : AdministrationController
    {
        private readonly IWorkoutService workouts;

        public WorkoutsController(IWorkoutService workouts)
            => this.workouts = workouts;

        public IActionResult Edit(int id)
        {
            var model = this.workouts.EditFormModel(id);

            if (model == null)
            {
                return this.BadRequest();
            }

            model.Sports = this.workouts.GetSports();
            model.BodyParts = this.workouts.GetBodyParts();

            return this.View(model);
        }

        [HttpPost]
        public IActionResult Edit(WorkoutFormModel model)
        {
            if (!Enum.IsDefined(typeof(DifficultLevel), model.DifficultLevel))
            {
                this.ModelState.AddModelError(nameof(model.DifficultLevel), "Invalid difficult level.");
            }

            if (!this.workouts.IsValidSportId(model.SportId))
            {
                this.ModelState.AddModelError(nameof(model.SportId), "Invalid sport.");
            }

            if (!this.workouts.IsValidBodyPartId(model.BodyPartId))
            {
                this.ModelState.AddModelError(nameof(model.BodyPartId), "Invalid body part.");
            }

            if (!this.ModelState.IsValid)
            {
                model.Sports = this.workouts.GetSports();
                model.BodyParts = this.workouts.GetBodyParts();
                return this.View(model);
            }

            this.workouts.Edit(model.Id, model.Title, model.SportId , (DifficultLevel)model.DifficultLevel, model.BodyPartId, model.Minutes, model.Content);

            return this.RedirectToAction("Details", "Workouts", new { area = "", Id = model.Id });
        }

        public IActionResult Delete(int id)
        {
            var isDeleted = this.workouts.Delete(id);

            if (!isDeleted)
            {
                return this.BadRequest();
            }

            return this.RedirectToAction("All", "Workouts", new { area = "" });
        }
    }
}
