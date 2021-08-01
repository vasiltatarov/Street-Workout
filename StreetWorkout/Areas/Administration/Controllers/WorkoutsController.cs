namespace StreetWorkout.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Services.Workouts;

    public class WorkoutsController : AdministrationController
    {
        private readonly IWorkoutService workouts;

        public WorkoutsController(IWorkoutService workouts)
            => this.workouts = workouts;

        public IActionResult Edit(int id)
        {
            return this.View();
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
