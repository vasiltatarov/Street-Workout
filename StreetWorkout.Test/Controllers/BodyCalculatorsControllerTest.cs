namespace StreetWorkout.Test.Controllers
{
    using Xunit;
    using MyTested.AspNetCore.Mvc;
    using StreetWorkout.Controllers;

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
