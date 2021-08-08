namespace StreetWorkout.Test.Routing
{
    using Xunit;
    using MyTested.AspNetCore.Mvc;
    using StreetWorkout.Controllers;
    using StreetWorkout.Services.Supplements.Models;

    public class SupplementsControllerTest
    {
        [Fact]
        public void AllShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Supplements/All")
                .To<SupplementsController>(c => c.All(With.Any<SupplementsQueryModel>()));

        [Fact]
        public void DetailsShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Supplements/Details/1")
                .To<SupplementsController>(c => c.Details(1));
    }
}
