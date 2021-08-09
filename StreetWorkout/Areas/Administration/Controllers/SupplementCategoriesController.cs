namespace StreetWorkout.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    using Services.SupplementCategories;

    using static WebConstants;

    [Authorize(Roles = AdministratorRoleName)]
    public class SupplementCategoriesController : AdministrationController
    {
        private readonly ISupplementCategoryService supplementCategories;

        public SupplementCategoriesController(ISupplementCategoryService supplementCategories)
        {
            this.supplementCategories = supplementCategories;
        }

        public IActionResult All()
            => this.View(this.supplementCategories.GetAll());

        public IActionResult Delete(int id)
        {
            if (!this.supplementCategories.Delete(id))
            {
                return this.BadRequest();
            }

            return this.RedirectToAction("All");
        }
    }
}
