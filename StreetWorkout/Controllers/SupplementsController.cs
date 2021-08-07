namespace StreetWorkout.Controllers
{
    using System.Linq;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Services.Supplements;
    using Services.Supplements.Models;

    using static WebConstants.TempDataMessageKeys;

    [Authorize]
    public class SupplementsController : Controller
    {
        private readonly ISupplementService supplements;

        public SupplementsController(ISupplementService supplements)
            => this.supplements = supplements;

        public IActionResult All([FromQuery]SupplementsQueryModel query)
        {
            var model = this.supplements.All(query.CurrentPage, query.SearchTerms, query.CategoryId);

            if (!model.Supplements.Any())
            {
                this.TempData[NotFoundSupplementsKey] = NotFoundSupplementsMessage;
            }

            return this.View(model);
        }

        public IActionResult Details(int id)
            => this.View(this.supplements.Details(id));
    }
}
