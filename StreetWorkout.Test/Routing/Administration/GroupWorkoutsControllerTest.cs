namespace StreetWorkout.Test.Routing.Administration
{
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    using StreetWorkout.Areas.Administration.Controllers;
    using ViewModels.GroupWorkouts;

    public class GroupWorkoutsControllerTest
    {
        [Fact]
        public void EditShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Administration/GroupWorkouts/Edit/1")
                .To<GroupWorkoutsController>(c => c.Edit(1));

        [Fact]
        public void EditShouldBeMappedAndShouldBeAllowedOnlyByPostRequest()
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithPath("/Administration/GroupWorkouts/Edit")
                    .WithMethod(HttpMethod.Post))
                .To<GroupWorkoutsController>(c => c.Edit(With.Any<GroupWorkoutFormModel>()));

        [Fact]
        public void DeleteShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Administration/GroupWorkouts/Delete/1")
                .To<GroupWorkoutsController>(c => c.Delete(1));
    }
}
