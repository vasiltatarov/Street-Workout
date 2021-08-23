namespace StreetWorkout.Test.Routing.Administration
{
    using MyTested.AspNetCore.Mvc;
    using StreetWorkout.Areas.Administration.Controllers;
    using StreetWorkout.ViewModels.Workouts;
    using Xunit;

    public class WorkoutsControllerTest
    {
        [Fact]
        public void EditShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Administration/Workouts/Edit/1")
                .To<WorkoutsController>(c => c.Edit(1));

        [Fact]
        public void EditShouldBeMappedOnPostRequest()
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithPath("/Administration/Workouts/Edit/1")
                    .WithMethod(HttpMethod.Post))
                .To<WorkoutsController>(c => c.Edit(With.Any<WorkoutFormModel>()));

        [Fact]
        public void DeleteShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Administration/Workouts/Delete/1")
                .To<WorkoutsController>(c => c.Delete(1));
    }
}
