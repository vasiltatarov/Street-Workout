namespace StreetWorkout.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using Services.Trainings;
    using ViewModels.Trainers;

    public class TrainersController : Controller
    {
        private readonly ITrainerService trainerService;

        public TrainersController(ITrainerService trainerService)
            => this.trainerService = trainerService;

        public IActionResult All([FromQuery]AllTrainersViewModel model)
            => this.View(this.trainerService.All(model.CurrentPage));
    }
}
