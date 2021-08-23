namespace StreetWorkout.Test.Routing.Api
{
    using MyTested.AspNetCore.Mvc;
    using StreetWorkout.Controllers.Api;
    using StreetWorkout.ViewModels.BodyCalculators;
    using Xunit;

    public class BodyCalculatorsApiControllerTest
    {
        [Fact]
        public void CalculateShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithPath("/api/calculator")
                    .WithMethod(HttpMethod.Post))
                .To<BodyCalculatorsApiController>(c => c.Calculate(With.Any<CalculatorFormModel>()));
    }
}
