namespace StreetWorkout.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using static WebConstants;

    [Authorize(Roles = AdministratorRoleName)]
    public class HomeController : AdministrationController
    {
        public IActionResult Index()
            => this.View();
    }
}
