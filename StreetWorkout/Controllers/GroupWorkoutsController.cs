namespace StreetWorkout.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using Infrastructure;
    using Services.Workouts;
    using ViewModels.GroupWorkouts;
    using Services.GroupWorkouts;
    using Services.GroupWorkouts.Models;

    using static WebConstants.ModelStateMessage;

    [Authorize]
    public class GroupWorkoutsController : Controller
    {
        private readonly IWorkoutService workouts;
        private readonly IGroupWorkoutService groupWorkouts;

        public GroupWorkoutsController(IWorkoutService workouts, IGroupWorkoutService groupWorkouts)
        {
            this.workouts = workouts;
            this.groupWorkouts = groupWorkouts;
        }

        public async Task<IActionResult> All([FromQuery] GroupWorkoutsQueryModel model)
            => this.View(await this.groupWorkouts.All(model.CurrentPage, this.User.GetId()));

        public async Task<IActionResult> Create()
            => this.View(new GroupWorkoutFormModel
            {
                Sports = await this.workouts.GetSports(),
            });

        [HttpPost]
        public async Task<IActionResult> Create(GroupWorkoutFormModel input)
        {
            var userId = this.User.GetId();

            if (!await this.groupWorkouts.IsUserTrainer(userId))
            {
                return this.Unauthorized();
            }

            if (!await this.workouts.IsValidSportId(input.SportId))
            {
                this.ModelState.AddModelError(nameof(input.SportId), InvalidSport);
            }

            if (input.StartOn <= DateTime.UtcNow || input.EndOn <= input.StartOn)
            {
                this.ModelState.AddModelError(nameof(input.StartOn), InvalidStartWorkoutTime);
            }

            if (input.EndOn <= DateTime.UtcNow || input.EndOn <= input.StartOn)
            {
                this.ModelState.AddModelError(nameof(input.EndOn), InvalidEndWorkoutTime);
            }

            if (!this.ModelState.IsValid)
            {
                input.Sports = await this.workouts.GetSports();
                return this.View(input);
            }

            await this.groupWorkouts.Create(input.Title, input.SportId, input.Address, input.StartOn, input.EndOn, input.MaximumParticipants, input.PricePerPerson, userId, input.Content);

            return this.RedirectToAction(nameof(All));
        }

        public async Task<IActionResult> Details(int id)
            => this.View(await this.groupWorkouts.Details(id));
    }
}
