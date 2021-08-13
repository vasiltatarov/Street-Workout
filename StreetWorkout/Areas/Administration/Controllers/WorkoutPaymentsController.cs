namespace StreetWorkout.Areas.Administration.Controllers
{
    using System.Threading.Tasks;
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

        public async Task<IActionResult> All()
            => this.View(await this.workoutPayments.All());
    }
}
