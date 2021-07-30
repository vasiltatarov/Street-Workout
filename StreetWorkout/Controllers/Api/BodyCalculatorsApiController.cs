namespace StreetWorkout.Controllers.Api
{
    using System;
    using Microsoft.AspNetCore.Mvc;

    using Data.Models.Enums;
    using ViewModels.BodyCalculators;

    [Route("api/calculator")]
    public class BodyCalculatorsApiController : ApiController
    {
        [HttpPost]
        public ActionResult<CalculatorResponseModel> Calculate(CalculatorFormModel model)
        {
            if (!Enum.IsDefined(typeof(Gender), model.Gender))
            {
                this.ModelState.AddModelError(nameof(model.Gender), "Invalid Gender type.");
            }

            var gender = (Gender)model.Gender;

            var genderValue = gender == Gender.Male ? 66 : 655.1;
            var weightInPounds = model.Weight * 2.20462262;
            var centimetersInInches = model.Height * 0.393700787;

            // Basal Metabolic Rate (BMR)
            double bmr;

            if (gender == Gender.Male)
            {
                bmr = genderValue + (6.2 * weightInPounds) + (12.7 * centimetersInInches) - (6.76 * model.Age);
            }
            else
            {
                bmr = genderValue + (4.35 * weightInPounds) + (4.7 * centimetersInInches) - (4.7 * model.Age);
            }

            var calories = (int)(bmr * model.Activity);

            return new CalculatorResponseModel()
            {
                Calories = calories,
            };
        }
    }
}
