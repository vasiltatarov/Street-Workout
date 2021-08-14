namespace StreetWorkout.Test.Controllers
{
    using System.Linq;
    using MyTested.AspNetCore.Mvc;
    using Shouldly;
    using Xunit;

    using Data;
    using StreetWorkout.Data.Models;
    using StreetWorkout.Controllers;
    using StreetWorkout.Services.Supplements.Models;

    using static WebConstants.TempDataMessageKeys;

    public class SupplementsControllerTest
    {
        [Theory]
        [InlineData("Amix")]
        public void DetailsShouldReturnViewWithCorrectSupplementServiceModel(string name)
            => MyController<SupplementsController>
                .Instance(c => c
                    .WithUser()
                    .WithData(data => data
                        .WithEntities(new Supplement
                        {
                            Name = name,
                            Category = new SupplementCategory(),
                        })))
                .Calling(c => c.Details(1))
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<SupplementServiceModel>()
                    .Passing(data =>
                    {
                        data.Name.ShouldBe(name);
                    }));

        [Fact]
        public void AllShouldReturnViewWithTempDataAndWithoutSupplements()
            => MyController<SupplementsController>
                .Instance(c => c
                    .WithUser())
                .Calling(c => c.All(With.Default<SupplementsQueryModel>()))
                .ShouldHave()
                .TempData(data => data
                    .ContainingEntryWithKey(NotFoundDataKey))
                .AndAlso()
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<SupplementsQueryModel>());

        [Fact]
        public void AllShouldReturnViewWithCorrectCountOfSupplements()
            => MyController<SupplementsController>
                .Instance(c => c
                    .WithUser()
                    .WithData(data => data
                        .WithEntities(Supplements.TenSupplements())))
                .Calling(c => c.All(With.Default<SupplementsQueryModel>()))
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<SupplementsQueryModel>()
                    .Passing(data =>
                    {
                        data.Supplements.Count().ShouldBe(8);
                        data.TotalSupplements.ShouldBe(10);
                        data.CurrentPage.ShouldBe(1);
                    }));
    }
}
