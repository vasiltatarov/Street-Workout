namespace StreetWorkout.Areas.Administration.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using StreetWorkout.Data.Models.Enums;
    using StreetWorkout.Infrastructure;
    using StreetWorkout.Services.Workouts;
    using StreetWorkout.ViewModels.Workouts;
    using static WebConstants.ModelStateMessage;
    using static WebConstants.TempDataMessageKeys;

    public class WorkoutsController : AdministrationController
    {
        private const string RedirectWorkoutsControllerName = "Workouts";
        private const string RedirectAllActionName = nameof(StreetWorkout.Controllers.WorkoutsController.All);
        private const string RedirectDetailsActionName = nameof(StreetWorkout.Controllers.WorkoutsController.Details);

        private readonly IWorkoutService workouts;

        public WorkoutsController(IWorkoutService workouts)
            => this.workouts = workouts;

        public async Task<IActionResult> Edit(int id)
        {
            if (!this.User.IsInRole(WebConstants.AdministratorRoleName) && !await this.workouts.IsUserCreator(this.User.GetId(), id))
            {
                return this.Unauthorized();
            }

            var model = await this.workouts.EditFormModel(id);

            if (model == null)
            {
                return this.BadRequest();
            }

            model.Sports = await this.workouts.GetSports();
            model.BodyParts = await this.workouts.GetBodyParts();

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(WorkoutFormModel model)
        {
            if (!this.User.IsInRole(WebConstants.AdministratorRoleName) && !await this.workouts.IsUserCreator(this.User.GetId(), model.Id))
            {
                return this.Unauthorized();
            }

            if (!Enum.IsDefined(typeof(DifficultLevel), model.DifficultLevel))
            {
                this.ModelState.AddModelError(nameof(model.DifficultLevel), InvalidDifficultLevel);
            }

            if (!await this.workouts.IsValidSportId(model.SportId))
            {
                this.ModelState.AddModelError(nameof(model.SportId), InvalidSport);
            }

            if (!await this.workouts.IsValidBodyPartId(model.BodyPartId))
            {
                this.ModelState.AddModelError(nameof(model.BodyPartId), InvalidBodyPart);
            }

            if (!this.ModelState.IsValid)
            {
                model.Sports = await this.workouts.GetSports();
                model.BodyParts = await this.workouts.GetBodyParts();
                return this.View(model);
            }

            await this.workouts.Edit(model.Id, model.Title, model.SportId, (DifficultLevel)model.DifficultLevel, model.BodyPartId, model.Minutes, model.Content);

            this.TempData[EditKey] = string.Format(EditMessage, model.Title);

            return this.RedirectToAction(RedirectDetailsActionName, RedirectWorkoutsControllerName, new { area = string.Empty, Id = model.Id, information = model.Title });
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (!this.User.IsInRole(WebConstants.AdministratorRoleName) && !await this.workouts.IsUserCreator(this.User.GetId(), id))
            {
                return this.Unauthorized();
            }

            var isDeleted = await this.workouts.Delete(id);

            if (!isDeleted)
            {
                return this.BadRequest();
            }

            return this.RedirectToAction(RedirectAllActionName, RedirectWorkoutsControllerName, new { area = string.Empty });
        }
    }
}
