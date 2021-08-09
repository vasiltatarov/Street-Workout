namespace StreetWorkout.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    using Services.Supplements;

    using static WebConstants;

    [Authorize(Roles = AdministratorRoleName)]
    public class SupplementCategoriesController : AdministrationController
    {
        private readonly ISupplementService supplements;

        public SupplementCategoriesController(ISupplementService supplements)
            => this.supplements = supplements;

        public IActionResult All()
            => this.View(this.supplements.GetSupplementCategories());
    }
}
