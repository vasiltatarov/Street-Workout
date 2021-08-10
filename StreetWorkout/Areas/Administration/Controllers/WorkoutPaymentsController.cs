namespace StreetWorkout.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using Services.WorkoutPayments;

    using static WebConstants;

    [Authorize(Roles = AdministratorRoleName)]
    public class WorkoutPaymentsController : AdministrationController
    {
        private readonly IWorkoutPaymentService workoutPayments;

        public WorkoutPaymentsController(IWorkoutPaymentService workoutPayments)
            => this.workoutPayments = workoutPayments;

        public IActionResult All()
            => this.View(this.workoutPayments.All());
    }
}
