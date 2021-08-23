namespace StreetWorkout.Test.Controllers.Api
{
    using System.Threading.Tasks;
    using MyTested.AspNetCore.Mvc;
    using Shouldly;
    using StreetWorkout.Controllers.Api;
    using StreetWorkout.Data.Models;
    using StreetWorkout.ViewModels.GroupWorkouts;
    using Xunit;

    public class GroupWorkoutsApiControllerTest
    {
        [Fact]
        public void BuyTicketShouldReturnActionResultWithGroupWorkoutResponseModel()
            => MyController<GroupWorkoutsApiController>
                .Instance()
                .WithUser()
                .WithData(data => data
                    .WithEntities(new GroupWorkout
                    {
                        Sport = new Sport(),
                        MaximumParticipants = 20,
                        Trainer = new ApplicationUser(),
                    }))
                .Calling(c => c.BuyTicket(new GroupWorkoutInputModel
                {
                    BoughtTickets = 10,
                    Card = string.Empty,
                    FullName = "test testov",
                    PhoneNumber = "08885948872",
                    GroupWorkoutId = 1,
                }).GetAwaiter().GetResult())
                .ShouldReturn()
                .ActionResult<GroupWorkoutResponseModel>(result => result
                    .Passing(data => ((int)data.AvailableTickets).ShouldBe(10)));

        [Fact]
        public void
            BuyTicketShouldReturnBadRequestWhenTryToBuyMoreTicketsThanTheAvailableAndShouldBeAllowedOnlyByPostRequest()
            => MyController<GroupWorkoutsApiController>
                .Instance()
                .WithUser()
                .WithData(data => data
                    .WithEntities(new GroupWorkout
                    {
                        Sport = new Sport(),
                        MaximumParticipants = 20,
                        Trainer = new ApplicationUser(),
                    }))
                .Calling(c => c.BuyTicket(new GroupWorkoutInputModel
                {
                    BoughtTickets = 22,
                    Card = string.Empty,
                    FullName = "test testov",
                    PhoneNumber = "08885948872",
                    GroupWorkoutId = 1,
                }))
                .ShouldHave()
                .ActionAttributes(attr => attr
                    .RestrictingForHttpMethod(HttpMethod.Post))
                .AndAlso()
                .ShouldReturn()
                .BadRequest();
    }
}
