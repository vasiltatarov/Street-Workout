namespace StreetWorkout.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using ViewModels.Supplements;
    using Services.Supplements;

    using static WebConstants;

    [Authorize]
    public class SupplementsController : Controller
    {
        private readonly ISupplementService supplements;

        public SupplementsController(ISupplementService supplements)
            => this.supplements = supplements;

        public IActionResult All()
        {
            return this.View();
        }

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
    }
}
