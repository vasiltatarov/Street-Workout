namespace StreetWorkout.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using Services;

    public class TrainersController : Controller
    {
        private readonly ITrainerService trainerService;

        public TrainersController(ITrainerService trainerService)
            => this.trainerService = trainerService;

        public IActionResult All()
            => this.View(this.trainerService.All());
    }
}
