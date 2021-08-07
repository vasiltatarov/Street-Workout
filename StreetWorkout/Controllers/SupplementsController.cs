namespace StreetWorkout.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Services.Supplements;
    using Services.Supplements.Models;

    [Authorize]
    public class SupplementsController : Controller
    {
        private readonly ISupplementService supplements;

        public SupplementsController(ISupplementService supplements)
            => this.supplements = supplements;

        public IActionResult All([FromQuery]SupplementsQueryModel query)
            => this.View(this.supplements.All(query.CurrentPage));

        public IActionResult Details(int id)
            => this.View(this.supplements.Details(id));
    }
}
