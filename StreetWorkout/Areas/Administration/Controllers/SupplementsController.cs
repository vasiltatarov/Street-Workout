namespace StreetWorkout.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    using Services.Supplements;
    using ViewModels.Supplements;

    using static WebConstants;
    using static WebConstants.TempDataMessageKeys;

    [Authorize(Roles = AdministratorRoleName)]
    public class SupplementsController : AdministrationController
    {
        private readonly ISupplementService supplements;

        public SupplementsController(ISupplementService supplements)
            => this.supplements = supplements;

        public IActionResult Create()
            => this.View(new SupplementFormModel
            {
                Categories = this.supplements.GetSupplementCategories(),
            });

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

        public IActionResult Edit(int id)
        {
            const string SupplementIdKey = "SupplementIdKey";

            var supplementFormModel = this.supplements.EditForModel(id);

            if (supplementFormModel == null)
            {
                return this.BadRequest();
            }

            supplementFormModel.Categories = this.supplements.GetSupplementCategories();

            this.TempData[SupplementIdKey] = id;

            return this.View(supplementFormModel);
        }

        [HttpPost]
        public IActionResult Edit(int id, SupplementFormModel model)
        {
            if (!this.supplements.IsValidCategoryId(model.CategoryId))
            {
                this.ModelState.AddModelError(nameof(model.CategoryId), "Invalid category.");
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var isEdited = this.supplements.Edit(id, model.Name, model.CategoryId, model.ImageUrl, model.Content, model.Price, model.Quantity);

            if (!isEdited)
            {
                return this.BadRequest();
            }

            this.TempData[EditKey] = string.Format(EditMessage, model.Name);

            return this.RedirectToAction("Details", "Supplements", new { area = "", Id = id });
        }

        public IActionResult Delete(int id)
        {
            var isDeleted = this.supplements.Delete(id);

            if (!isDeleted)
            {
                return this.BadRequest();
            }

            this.TempData[DeleteKey] = string.Format(DeleteMessage, "Supplement");

            return this.RedirectToAction("All", "Supplements", new { area = "" });
        }
    }
}
