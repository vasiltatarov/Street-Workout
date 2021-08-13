namespace StreetWorkout.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;

    using Services.Trainings;
    using ViewModels.Trainers;

    public class TrainersController : Controller
    {
        private readonly ITrainerService trainerService;

        public TrainersController(ITrainerService trainerService)
            => this.trainerService = trainerService;

        public async Task<IActionResult> All([FromQuery]AllTrainersViewModel model)
            => this.View(await this.trainerService.All(model.CurrentPage));
    }
}
