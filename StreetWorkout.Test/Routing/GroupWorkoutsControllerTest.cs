namespace StreetWorkout.Test.Routing
{
    using Xunit;
    using MyTested.AspNetCore.Mvc;
    using StreetWorkout.Controllers;
    using StreetWorkout.Services.GroupWorkouts.Models;
    using ViewModels.GroupWorkouts;

    public class GroupWorkoutsControllerTest
    {
        [Fact]
        public void AllShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/GroupWorkouts/All")
                .To<GroupWorkoutsController>(c => c.All(With.Any<GroupWorkoutsQueryModel>()));

        [Fact]
        public void CreateShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/GroupWorkouts/Create")
                .To<GroupWorkoutsController>(c => c.Create());

        [Fact]
        public void CreateShouldBeMappedOnMethodPost()
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithPath("/GroupWorkouts/Create")
                    .WithMethod(HttpMethod.Post))
                .To<GroupWorkoutsController>(c => c.Create(With.Any<GroupWorkoutFormModel>()));

        [Fact]
        public void DetailsShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/GroupWorkouts/Details/1")
                .To<GroupWorkoutsController>(c => c.Details(1));
    }
}
