namespace StreetWorkout.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using ViewModels.Supplements;
    using Services.Supplements;
    using Services.Supplements.Models;

    using static WebConstants;

    [Authorize]
    public class SupplementsController : Controller
    {
        private readonly ISupplementService supplements;

        public SupplementsController(ISupplementService supplements)
            => this.supplements = supplements;

        public IActionResult All([FromQuery]SupplementsQueryModel query)
            => this.View(this.supplements.All(query.CurrentPage));

        [Authorize(Roles = AdministratorRoleName)]
        public IActionResult Create()
            => this.View(new SupplementFormModel
            {
                Categories = this.supplements.GetSupplementCategories(),
            });

        [Authorize(Roles = AdministratorRoleName)]
        [HttpPost]
        public IActionResult Create(SupplementFormModel model)
        {
            if (!this.supplements.IsValidCategoryId(model.CategoryId))
            {
                this.ModelState.AddModelError(nameof(model.CategoryId), "Invalid category.");
            }

            if (!this.ModelState.IsValid)
            {
                model.Categories = this.supplements.GetSupplementCategories();
                return this.View(model);
            }

            this.supplements.Create(model.Name, model.CategoryId, model.ImageUrl, model.Content, model.Price, model.Quantity);

            return this.RedirectToAction("All");
        }

        public IActionResult Details(int id)
            => this.View(this.supplements.Details(id));
    }
}
