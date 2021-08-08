namespace StreetWorkout.Test.Routing
{
    using Xunit;
    using MyTested.AspNetCore.Mvc;
    using StreetWorkout.Controllers;
    using ViewModels.Trainers;

    public class TrainersControllerTest
    {
        [Fact]
        public void AllShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Trainers/All")
                .To<TrainersController>(c => c.All(With.Any<AllTrainersViewModel>()));
    }
}
