namespace StreetWorkout.Test.Routing.Api
{
    using Xunit;
    using MyTested.AspNetCore.Mvc;
    using StreetWorkout.Controllers.Api;
    using ViewModels.BodyCalculators;

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
