namespace StreetWorkout.Test.Routing.Api
{
    using Xunit;
    using MyTested.AspNetCore.Mvc;
    using StreetWorkout.Controllers.Api;
    using ViewModels.GroupWorkouts;

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
