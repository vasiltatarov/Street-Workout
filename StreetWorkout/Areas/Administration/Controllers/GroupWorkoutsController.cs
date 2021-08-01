using Microsoft.AspNetCore.Mvc;

namespace StreetWorkout.Areas.Administration.Controllers
{
    public class GroupWorkoutsController : AdministrationController
    {
        public GroupWorkoutsController()
        {
            
        }

        public IActionResult Edit(int id)
        {
            return this.View();
        }
    }
}
