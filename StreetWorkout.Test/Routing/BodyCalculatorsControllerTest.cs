namespace StreetWorkout.Test.Routing
{
    using MyTested.AspNetCore.Mvc;
    using StreetWorkout.Controllers;
    using Xunit;

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
