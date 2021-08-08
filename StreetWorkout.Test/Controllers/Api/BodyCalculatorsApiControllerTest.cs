namespace StreetWorkout.Test.Controllers.Api
{
    using MyTested.AspNetCore.Mvc;
    using Shouldly;
    using Xunit;
    using StreetWorkout.Controllers.Api;
    using ViewModels.BodyCalculators;

    public class BodyCalculatorsApiControllerTest
    {
        [Fact]
        public void CalculateShould()
            => MyController<BodyCalculatorsApiController>
                .Instance()
                .WithUser()
                .Calling(c => c.Calculate(new CalculatorFormModel
                {
                    Weight = 50,
                    Gender = 1,
                    Activity = 1.2,
                    Age = 21,
                    Height = 150,
                }))
                .ShouldHave()
                .ActionAttributes(attr => attr
                    .RestrictingForHttpMethod(HttpMethod.Post))
                .AndAlso()
                .ShouldReturn()
                .ActionResult<CalculatorResponseModel>(result => result
                    .Passing(data =>
                    {
                        data.Calories.ShouldBeGreaterThan(0);
                    }));

    }
}
