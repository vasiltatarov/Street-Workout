namespace StreetWorkout.Areas.Administration.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;

    using ViewModels.GroupWorkouts;
    using Services.GroupWorkouts;
    using Services.Workouts;
    using Infrastructure;

    using static WebConstants.ModelStateMessage;

    public class GroupWorkoutsController : AdministrationController
    {
        private const string RedirectDetailsActionName = nameof(StreetWorkout.Controllers.GroupWorkoutsController.Details);
        private const string RedirectAllActionName = nameof(StreetWorkout.Controllers.GroupWorkoutsController.All);
        private const string RedirectGroupWorkoutsControllerName = "GroupWorkouts";

        private readonly IGroupWorkoutService groupWorkouts;
        private readonly IWorkoutService workouts;

        public GroupWorkoutsController(IGroupWorkoutService groupWorkouts, IWorkoutService workouts)
        {
            this.groupWorkouts = groupWorkouts;
            this.workouts = workouts;
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (!this.User.IsInRole(WebConstants.AdministratorRoleName) && !await this.groupWorkouts.IsUserCreator(this.User.GetId(), id))
            {
                return this.Unauthorized();
            }

            var model = await this.groupWorkouts.EditFormModel(id);
            model.Sports = await this.workouts.GetSports();

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(GroupWorkoutFormModel model)
        {
            if (!this.User.IsInRole(WebConstants.AdministratorRoleName) && !await this.groupWorkouts.IsUserCreator(this.User.GetId(), model.Id))
            {
                return this.Unauthorized();
            }

            if (!await this.groupWorkouts.IsUserTrainer(this.User.GetId()))
            {
                return this.Unauthorized();
            }

            if (!await this.workouts.IsValidSportId(model.SportId))
            {
                this.ModelState.AddModelError(nameof(model.SportId), InvalidSport);
            }

            if (model.StartOn <= DateTime.UtcNow || model.EndOn <= model.StartOn)
            {
                this.ModelState.AddModelError(nameof(model.StartOn), InvalidStartWorkoutTime);
            }

            if (model.EndOn <= DateTime.UtcNow || model.EndOn <= model.StartOn)
            {
                this.ModelState.AddModelError(nameof(model.EndOn), InvalidEndWorkoutTime);
            }

            if (!this.ModelState.IsValid)
            {
                model.Sports = await this.workouts.GetSports();
                return this.View(model);
            }

            await this.groupWorkouts.Edit(model.Id, model.Title, model.SportId, model.Address, model.StartOn, model.EndOn, model.MaximumParticipants, model.PricePerPerson, model.Content);

            return this.RedirectToAction(RedirectDetailsActionName, RedirectGroupWorkoutsControllerName, new { area = string.Empty, Id = model.Id });
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (!this.User.IsInRole(WebConstants.AdministratorRoleName) && !await this.groupWorkouts.IsUserCreator(this.User.GetId(), id))
            {
                return this.Unauthorized();
            }

            if (!await this.groupWorkouts.Delete(id))
            {
                return this.BadRequest();
            }

            return this.RedirectToAction(RedirectAllActionName, RedirectGroupWorkoutsControllerName, new { area = string.Empty });
        }
    }
}
