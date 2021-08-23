namespace StreetWorkout.Test.Routing.Administration
{
    using MyTested.AspNetCore.Mvc;
    using StreetWorkout.Areas.Administration.Controllers;
    using Xunit;

    public class WorkoutPaymentsControllerTest
    {
        [Fact]
        public void AllShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Administration/WorkoutPayments/All")
                .To<WorkoutPaymentsController>(c => c.All());
    }
}
