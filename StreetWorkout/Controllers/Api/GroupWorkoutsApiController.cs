namespace StreetWorkout.Controllers.Api
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;

    using Infrastructure;
    using Services.GroupWorkouts;
    using ViewModels.GroupWorkouts;

    [Route("api/workouts")]
    public class GroupWorkoutsApiController : ApiController
    {
        private readonly IGroupWorkoutService groupWorkouts;

        public GroupWorkoutsApiController(IGroupWorkoutService groupWorkouts)
            => this.groupWorkouts = groupWorkouts;

        [HttpPost]
        public async Task<ActionResult<GroupWorkoutResponseModel>> BuyTicket(GroupWorkoutInputModel model)
        {
            var availableTickets = await this.groupWorkouts.AvailableTickets(model.GroupWorkoutId);

            if (model.BoughtTickets > availableTickets)
            {
                return this.BadRequest();
            }

            await this.groupWorkouts.BuyTicket(this.User.GetId(), model.GroupWorkoutId, model.FullName, model.PhoneNumber, model.Card, model.BoughtTickets);

            return new GroupWorkoutResponseModel()
            {
                AvailableTickets = await this.groupWorkouts.AvailableTickets(model.GroupWorkoutId),
            };
        }
    }
}
