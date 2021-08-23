namespace StreetWorkout.Test.Routing
{
    using MyTested.AspNetCore.Mvc;
    using StreetWorkout.Controllers;
    using StreetWorkout.ViewModels.Trainers;
    using Xunit;

    public class TrainersControllerTest
    {
        [Fact]
        public void AllShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Trainers/All")
                .To<TrainersController>(c => c.All(With.Any<AllUsersQueryModel>()));
    }
}
