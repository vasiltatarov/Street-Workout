namespace StreetWorkout.Test.Routing
{
    using Xunit;
    using MyTested.AspNetCore.Mvc;
    using StreetWorkout.Controllers;

    public class BodyCalculatorsControllerTest
    {
        [Fact]
        public void CalculateShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/BodyCalculators/Calculate")
                .To<BodyCalculatorsController>(c => c.Calculate());
    }
}
