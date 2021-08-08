namespace StreetWorkout.Test.Routing
{
    using Xunit;
    using MyTested.AspNetCore.Mvc;
    using StreetWorkout.Controllers;

    public class HomeControllerTest
    {
        [Fact]
        public void IndexRouteShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/")
                .To<HomeController>(c => c.Index());

        [Fact]
        public void PrivacyShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Home/Privacy")
                .To<HomeController>(c => c.Privacy());

        [Fact]
        public void ErrorShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Home/Error")
                .To<HomeController>(c => c.Error());
    }
}
