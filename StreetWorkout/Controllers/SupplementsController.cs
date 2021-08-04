namespace StreetWorkout.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using StreetWorkout.ViewModels.Supplements;

    using static WebConstants;

    [Authorize]
    public class SupplementsController : Controller
    {
        public IActionResult All()
        {
            return this.View();
        }

        [Authorize(Roles = AdministratorRoleName)]
        public IActionResult Create()
        {
            // implement service and get supplements categoris from it
            return this.View(new SupplementFormModel());
        }

        [HttpPost]
        public IActionResult Create(SupplementFormModel model)
        {
            return this.RedirectToAction();
        }
    }
}
