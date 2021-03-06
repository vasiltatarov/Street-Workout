namespace StreetWorkout.Test.Routing
{
    using MyTested.AspNetCore.Mvc;
    using StreetWorkout.Controllers;
    using StreetWorkout.Services.Workouts.Models;
    using StreetWorkout.ViewModels.Workouts;
    using Xunit;

    public class WorkoutsControllerTest
    {
        [Fact]
        public void AllShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Workouts/All")
                .To<WorkoutsController>(c => c.All(With.Any<WorkoutsQueryModel>()));

        [Fact]
        public void DetailsShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Workouts/Details/1/information")
                .To<WorkoutsController>(c => c.Details(1, "information"));

        [Fact]
        public void CreateShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Workouts/Create")
                .To<WorkoutsController>(c => c.Create());

        [Fact]
        public void CreateShouldBeMappedOnPostMethod()
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithPath("/Workouts/Create")
                    .WithMethod(HttpMethod.Post))
                .To<WorkoutsController>(c => c.Create(With.Any<WorkoutFormModel>()));
    }
}
