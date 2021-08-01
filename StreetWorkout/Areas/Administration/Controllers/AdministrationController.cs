﻿namespace StreetWorkout.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using static WebConstants;

    [Authorize(Roles = AdministratorRoleName)]
    [Area(AreaName)]
    public class AdministrationController : Controller
    {
    }
}
