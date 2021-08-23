namespace StreetWorkout.Test.Controllers.Administration
{
    using MyTested.AspNetCore.Mvc;
    using StreetWorkout.Areas.Administration.Controllers;
    using Xunit;

    public class HomeControllerTest
    {
        [Fact]
        public void IndexShouldReturnView()
            => MyController<HomeController>
                .Instance()
                .WithUser()
                .Calling(c => c.Index())
                .ShouldReturn()
                .View();
    }
}
