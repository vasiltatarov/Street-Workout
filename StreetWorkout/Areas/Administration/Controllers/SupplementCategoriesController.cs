namespace StreetWorkout.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    using Services.SupplementCategories;
    using Services.SupplementCategories.Models;

    using static WebConstants;
    using static WebConstants.TempDataMessageKeys;

    [Authorize(Roles = AdministratorRoleName)]
    public class SupplementCategoriesController : AdministrationController
    {
        private readonly ISupplementCategoryService supplementCategories;

        public SupplementCategoriesController(ISupplementCategoryService supplementCategories)
            => this.supplementCategories = supplementCategories;

        public IActionResult All()
            => this.View(this.supplementCategories.GetAll());

        public IActionResult Delete(int id)
        {
            if (!this.supplementCategories.Delete(id))
            {
                return this.BadRequest();
            }

            this.TempData[DeleteKey] = string.Format(DeleteMessage, "Supplement Category");

            return this.RedirectToAction("All");
        }

        public IActionResult Restore(int id)
        {
            if (!this.supplementCategories.Restore(id))
            {
                return this.BadRequest();
            }

            this.TempData[RestoreKey] = string.Format(RestoreMessage, "Supplement Category");

            return this.RedirectToAction("All");
        }

        public IActionResult Edit(int id)
            => this.View(this.supplementCategories.GetSupplementCategoryEditModel(id));

        [HttpPost]
        public IActionResult Edit(int id, SupplementCategoryEditServiceModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            if (!this.supplementCategories.Edit(id, model.Name))
            {
                return this.BadRequest();
            }

            this.TempData[EditKey] = string.Format(EditMessage, model.Name);

            return this.RedirectToAction("All");
        }
    }
}
