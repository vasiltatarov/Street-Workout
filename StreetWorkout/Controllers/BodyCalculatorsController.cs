namespace StreetWorkout.Controllers
{
    using System;
    using Microsoft.AspNetCore.Mvc;

    using Data.Models.Enums;
    using ViewModels.BodyCalculators;

    public class BodyCalculatorsController : Controller
    {
        public IActionResult Calculate()
            => this.View();

        //[HttpPost]
        //public IActionResult Calculate(CalculatorFormModel model)
        //{
        //    if (!Enum.IsDefined(typeof(Gender), model.Gender))
        //    {
        //        this.ModelState.AddModelError(nameof(model.Gender), "Invalid Gender type.");
        //    }

        //    if (!this.ModelState.IsValid)
        //    {
        //        return this.View(model);
        //    }

        //    var gender = (Gender) model.Gender;

        //    var genderValue = gender == Gender.Male ? 66 : 655.1;
        //    var weightInPounds = model.Weight * 2.20462262;
        //    var centimetersInInches = model.Height * 0.393700787;

        //    // Basal Metabolic Rate (BMR)
        //    double bmr;

        //    if (gender == Gender.Male)
        //    {
        //        bmr = genderValue + (6.2 * weightInPounds) + (12.7 * centimetersInInches) - (6.76 * model.Age);
        //    }
        //    else
        //    {
        //        bmr = genderValue + (4.35 * weightInPounds) + (4.7 * centimetersInInches) - (4.7 * model.Age);
        //    }

        //    var calories = (int)(bmr * model.Activity);

        //    this.ViewData["Calories"] = calories;

        //    return this.View(model);
        //}
    }
}
