namespace StreetWorkout.Test.Controllers
{
    using MyTested.AspNetCore.Mvc;
    using StreetWorkout.Controllers;
    using Xunit;

    public class BodyCalculatorsControllerTest
    {
        [Fact]
        public void CalculateShouldReturnView()
            => MyController<BodyCalculatorsController>
                .Calling(c => c.Calculate())
                .ShouldReturn()
                .View();
    }
}
