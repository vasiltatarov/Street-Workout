namespace StreetWorkout.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class BodyCalculatorsController : Controller
    {
        public IActionResult Calculate()
            => this.View();
    }
}
