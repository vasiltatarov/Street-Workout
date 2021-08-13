namespace StreetWorkout.Areas.Administration.Controllers
{
    using System.Threading.Tasks;
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

        public async Task<IActionResult> All()
            => this.View(await this.supplementCategories.GetAll());

        public async Task<IActionResult> Delete(int id)
        {
            if (!await this.supplementCategories.Delete(id))
            {
                return this.BadRequest();
            }

            this.TempData[DeleteKey] = string.Format(DeleteMessage, "Supplement Category");

            return this.RedirectToAction("All");
        }

        public async Task<IActionResult> Restore(int id)
        {
            if (!await this.supplementCategories.Restore(id))
            {
                return this.BadRequest();
            }

            this.TempData[RestoreKey] = string.Format(RestoreMessage, "Supplement Category");

            return this.RedirectToAction("All");
        }

        public async Task<IActionResult> Edit(int id)
            => this.View(await this.supplementCategories.GetSupplementCategoryEditModel(id));

        [HttpPost]
        public async Task<IActionResult> Edit(int id, SupplementCategoryEditServiceModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            if (!await this.supplementCategories.Edit(id, model.Name))
            {
                return this.BadRequest();
            }

            this.TempData[EditKey] = string.Format(EditMessage, model.Name);

            return this.RedirectToAction("All");
        }
    }
}
