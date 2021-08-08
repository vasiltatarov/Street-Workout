﻿namespace StreetWorkout.Infrastructure
{
    using System;
    using Microsoft.AspNetCore.Mvc;

    public static class ControllerExtensions
    {
        public static string GetControllerName(this Type controllerType)
            => controllerType.Name.Replace(nameof(Controller), string.Empty);
    }
}
