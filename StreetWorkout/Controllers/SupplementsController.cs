namespace StreetWorkout.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using Services.Supplements;
    using Services.Supplements.Models;
    using Infrastructure;

    using static WebConstants.TempDataMessageKeys;

    [Authorize]
    public class SupplementsController : Controller
    {
        private readonly ISupplementService supplements;

        public SupplementsController(ISupplementService supplements)
            => this.supplements = supplements;

        public async Task<IActionResult> All([FromQuery]SupplementsQueryModel query)
        {
            var model = await this.supplements.All(query.CurrentPage, query.SearchTerms, query.CategoryId);

            if (model.TotalSupplements == 0)
            {
                this.TempData[NotFoundDataKey] = string.Format(NotFoundDataMessage, typeof(SupplementsController).GetControllerName());
            }

            return this.View(model);
        }

        public async Task<IActionResult> Details(int id)
            => this.View(await this.supplements.Details(id));
    }
}
