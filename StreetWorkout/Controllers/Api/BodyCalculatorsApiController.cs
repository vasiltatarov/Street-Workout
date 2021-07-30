namespace StreetWorkout.Controllers.Api
{
    using System;
    using Microsoft.AspNetCore.Mvc;

    using Data.Models.Enums;
    using Services.BodyCalculators;
    using ViewModels.BodyCalculators;

    [Route("api/calculator")]
    public class BodyCalculatorsApiController : ApiController
    {
        private readonly IBodyCalculatorService bodyCalculator;

        public BodyCalculatorsApiController(IBodyCalculatorService bodyCalculator)
            => this.bodyCalculator = bodyCalculator;

        [HttpPost]
        public ActionResult<CalculatorResponseModel> Calculate(CalculatorFormModel model)
        {
            if (!Enum.IsDefined(typeof(Gender), model.Gender))
            {
                this.ModelState.AddModelError(nameof(model.Gender), "Invalid Gender type.");
            }

            return new CalculatorResponseModel()
            {
                Calories = this.bodyCalculator.CalculateCalories(model.Weight, model.Height, model.Activity, model.Age, (Gender)model.Gender),
            };
        }
    }
}
