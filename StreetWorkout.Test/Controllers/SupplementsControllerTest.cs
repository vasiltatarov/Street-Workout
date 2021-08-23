namespace StreetWorkout.Test.Controllers
{
    using System.Linq;
    using MyTested.AspNetCore.Mvc;
    using Shouldly;
    using StreetWorkout.Controllers;
    using StreetWorkout.Data.Models;
    using StreetWorkout.Services.Supplements.Models;
    using StreetWorkout.Test.Data;
    using StreetWorkout.ViewModels.Supplements;
    using Xunit;
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

        [Theory]
        [InlineData(1, "amix")]
        public void BuyShouldReturnViewWithSupplementModelAndShouldBeAllowedByGetRequest(int id, string name)
            => MyController<SupplementsController>
                .Instance()
                .WithUser()
                .WithData(data => data
                    .WithEntities(new Supplement
                    {
                        Category = new SupplementCategory(),
                        Name = name,
                    }))
                .Calling(c => c.Buy(id).GetAwaiter().GetResult())
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<BuySupplementFormModel>()
                    .Passing(data =>
                    {
                        data.ShouldNotBeNull();
                        ((int)data.DeliveryPrice).ShouldBe(4);
                        ((int)data.VAT).ShouldBe(1);
                        data.SupplementModel.Name.ShouldBe(name);
                    }));

        [Fact]
        public void BuyShouldRedirectToActionAndShouldBeAllowedOnlyByPostRequest()
            => MyController<SupplementsController>
                .Instance()
                .WithUser()
                .WithData(data => data
                    .WithEntities(new Supplement
                    {
                        Category = new SupplementCategory(),
                        Name = "test",
                    }))
                .Calling(c => c.Buy(new BuySupplementFormModel
                {
                    FirstName = "Test",
                    LastName = "test",
                    Address = "test testov",
                    CardName = "onepay",
                    CardNumber = "1233412",
                    PhoneNumber = "0894367875",
                    Expiration = "04/23",
                    SupplementId = 1,
                }))
                .ShouldHave()
                .ActionAttributes(attr => attr
                    .RestrictingForHttpMethod(HttpMethod.Post))
                .AndAlso()
                .ShouldReturn()
                .RedirectToAction("ThankYou");

        [Fact]
        public void BuyShouldReturnViewWhenModelStateIsInvalidAndShouldBeAllowedOnlyByPostRequest()
            => MyController<SupplementsController>
                .Instance()
                .WithUser()
                .WithData(data => data
                    .WithEntities(new Supplement
                    {
                        Category = new SupplementCategory(),
                        Name = "test",
                    }))
                .Calling(c => c.Buy(new BuySupplementFormModel
                {
                    FirstName = "Test",
                    SupplementId = 1,
                }))
                .ShouldHave()
                .ActionAttributes(attr => attr
                    .RestrictingForHttpMethod(HttpMethod.Post))
                .AndAlso()
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<BuySupplementFormModel>()
                    .Passing(data =>
                    {
                        data.FirstName.ShouldBe("Test");
                        data.SupplementId.ShouldBe(1);
                        data.LastName.ShouldBeNull();
                    }));

        [Fact]
        public void BuyShouldReturnBadRequestAndShouldBeAllowedOnlyByPostRequest()
            => MyController<SupplementsController>
                .Instance()
                .WithUser()
                .Calling(c => c.Buy(new BuySupplementFormModel
                {
                    FirstName = "Test",
                    LastName = "test",
                    Address = "test testov",
                    CardName = "onepay",
                    CardNumber = "1233412",
                    PhoneNumber = "0894367875",
                    Expiration = "04/23",
                    SupplementId = 1,
                }))
                .ShouldHave()
                .ActionAttributes(attr => attr
                    .RestrictingForHttpMethod(HttpMethod.Post))
                .AndAlso()
                .ShouldReturn()
                .BadRequest();

        [Fact]
        public void ThankYouShouldReturnView()
            => MyController<SupplementsController>
                .Instance()
                .WithUser()
                .Calling(c => c.ThankYou())
                .ShouldReturn()
                .View();
    }
}
