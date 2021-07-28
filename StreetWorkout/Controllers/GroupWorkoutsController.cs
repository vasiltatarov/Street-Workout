namespace StreetWorkout.Controllers
{
    using System;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using Infrastructure;
    using Services.Workouts;
    using ViewModels.GroupWorkouts;
    using Services.GroupWorkouts;

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

        public IActionResult All([FromQuery] GroupWorkoutsQueryModel model)
            => this.View(this.groupWorkouts.All(model.CurrentPage, this.User.GetId()));

        public IActionResult Create()
            => this.View(new GroupWorkoutFormModel
            {
                Sports = this.workouts.GetSports(),
            });

        [HttpPost]
        public IActionResult Create(GroupWorkoutFormModel input)
        {
            var userId = this.User.GetId();

            if (!this.groupWorkouts.IsUserTrainer(userId))
            {
                return this.Unauthorized();
            }

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

            this.groupWorkouts.Create(input.Title, input.SportId, input.Address, input.StartOn, input.EndOn, input.MaximumParticipants, input.PricePerPerson, userId, input.Content);

            return this.RedirectToAction("All");
        }

        public IActionResult Details(int id)
            => this.View(this.groupWorkouts.Details(id));
    }
}
