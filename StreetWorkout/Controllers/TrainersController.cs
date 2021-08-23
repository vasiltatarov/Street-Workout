namespace StreetWorkout.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;

    using StreetWorkout.Services.Trainings;
    using StreetWorkout.ViewModels.Trainers;

    public class TrainersController : Controller
    {
        private readonly ITrainerService trainerService;

        public TrainersController(ITrainerService trainerService)
            => this.trainerService = trainerService;

        public async Task<IActionResult> All([FromQuery] AllUsersQueryModel model)
            => this.View(await this.trainerService.All(model.CurrentPage, model.Role, model.Sport));
    }
}
