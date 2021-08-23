namespace StreetWorkout.Test.Routing.Administration
{
    using MyTested.AspNetCore.Mvc;
    using StreetWorkout.Areas.Administration.Controllers;
    using Xunit;

    public class HomeControllerTest
    {
        [Fact]
        public void IndexShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Administration/Home/Index")
                .To<HomeController>(c => c.Index());
    }
}
