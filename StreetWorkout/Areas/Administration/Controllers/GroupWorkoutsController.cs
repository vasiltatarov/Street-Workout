namespace StreetWorkout.Areas.Administration.Controllers
{
    using System;
    using System.Threading.Tasks;
    using ViewModels.GroupWorkouts;
    using Microsoft.AspNetCore.Mvc;
    using Services.GroupWorkouts;
    using Services.Workouts;
    using Infrastructure;

    public class GroupWorkoutsController : AdministrationController
    {
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
            model.Sports = this.workouts.GetSports();

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

            if (!this.workouts.IsValidSportId(model.SportId))
            {
                this.ModelState.AddModelError(nameof(model.SportId), "Sport is invalid.");
            }

            if (model.StartOn <= DateTime.UtcNow || model.EndOn <= model.StartOn)
            {
                this.ModelState.AddModelError(nameof(model.StartOn), "Start Workout Time must be bigger than now. Also End Time must be bigger than the Start Time");
            }

            if (model.EndOn <= DateTime.UtcNow || model.EndOn <= model.StartOn)
            {
                this.ModelState.AddModelError(nameof(model.EndOn), "End Workout Time must be bigger than now. Also End Time must be bigger than the Start Time");
            }

            if (!this.ModelState.IsValid)
            {
                model.Sports = this.workouts.GetSports();
                return this.View(model);
            }

            await this.groupWorkouts.Edit(model.Id, model.Title, model.SportId, model.Address, model.StartOn, model.EndOn, model.MaximumParticipants, model.PricePerPerson, model.Content);

            return this.RedirectToAction("Details", "GroupWorkouts", new { area = "", Id = model.Id });
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

            return this.RedirectToAction("All", "GroupWorkouts", new { area = "" });
        }
    }
}
