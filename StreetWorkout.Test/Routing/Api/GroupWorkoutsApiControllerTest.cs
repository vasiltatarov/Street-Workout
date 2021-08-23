namespace StreetWorkout.Test.Routing.Api
{
    using MyTested.AspNetCore.Mvc;
    using StreetWorkout.Controllers.Api;
    using StreetWorkout.ViewModels.GroupWorkouts;
    using Xunit;

    public class GroupWorkoutsApiControllerTest
    {
        [Fact]
        public void BuyTicketShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithPath("/api/workouts")
                    .WithMethod(HttpMethod.Post))
                .To<GroupWorkoutsApiController>(c => c.BuyTicket(With.Any<GroupWorkoutInputModel>()));
    }
}
