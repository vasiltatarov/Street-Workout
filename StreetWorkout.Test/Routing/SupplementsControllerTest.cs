using StreetWorkout.ViewModels.Supplements;

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

        [Fact]
        public void BuyShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Supplements/Buy/1")
                .To<SupplementsController>(c => c.Buy(1));

        [Fact]
        public void BuyShouldBeMappedOnPostMethod()
            => MyRouting
                .Configuration()
                .ShouldMap("/Supplements/Buy")
                .To<SupplementsController>(c => c.Buy(With.Any<BuySupplementFormModel>()));

        [Fact]
        public void ThankYouShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Supplements/ThankYou")
                .To<SupplementsController>(c => c.ThankYou());
    }
}
