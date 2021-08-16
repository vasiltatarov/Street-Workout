namespace StreetWorkout.Test.Controllers.Administration
{
    using System.Collections.Generic;
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    using StreetWorkout.Areas.Administration.Controllers;
    using StreetWorkout.Services.WorkoutPayments.Models;

    using static WebConstants;

    public class WorkoutPaymentsControllerTest
    {
        [Fact]
        public void AllShouldReturnViewWithWorkoutsModel()
            => MyController<WorkoutPaymentsController>
                .Instance()
                .WithUser(user => user
                    .InRole(AdministratorRoleName))
                .Calling(c => c.All())
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<IEnumerable<UserWorkoutPaymentServiceModel>>());
    }
}
