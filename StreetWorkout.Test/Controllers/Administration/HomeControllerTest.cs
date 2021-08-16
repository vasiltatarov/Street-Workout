namespace StreetWorkout.Test.Controllers.Administration
{
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    using StreetWorkout.Areas.Administration.Controllers;

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
