namespace StreetWorkout.Test.Controllers.Administration
{
    using MyTested.AspNetCore.Mvc;
    using Shouldly;
    using Xunit;

    using StreetWorkout.Areas.Administration.Controllers;
    using ViewModels.Supplements;
    using StreetWorkout.Data.Models;

    using static WebConstants;
    using static WebConstants.TempDataMessageKeys;

    public class SupplementsControllerTest
    {
        [Fact]
        public void CreateShouldReturnViewWithCorrectFormModel()
            => MyController<SupplementsController>
                .Instance()
                .WithUser(user => user
                    .InRole(AdministratorRoleName))
                .Calling(c => c.Create())
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<SupplementFormModel>());

        [Theory]
        [InlineData("contentTest", "https://addons.cdn.mozilla.net/user-media/previews/full/230/230000.png?modified=1622132702", "amixTest")]
        public void CreateShouldReturnRedirectToActionWithValidModelStateAlsoShouldBeAllowedOnlyByPostRequest(
            string content,
            string imageUrl,
            string name)
            => MyController<SupplementsController>
                .Instance()
                .WithUser(user => user
                    .InRole(AdministratorRoleName))
                .WithData(data => data
                    .WithEntities(new SupplementCategory()))
                .Calling(c => c.Create(new SupplementFormModel
                {
                    Name = name,
                    CategoryId = 1,
                    Content = content,
                    ImageUrl = imageUrl,
                    Price = 50,
                    Quantity = 500,
                }))
                .ShouldHave()
                .ActionAttributes(attr => attr
                    .RestrictingForHttpMethod(HttpMethod.Post))
                .AndAlso()
                .ShouldReturn()
                .RedirectToAction("All");

        [Theory]
        [InlineData("contentTest", "miss", "amixTest")]
        public void CreateShouldReturnViewWhenModelStateIsInvalidAlsoShouldBeAllowedOnlyByPostRequest(
            string content,
            string imageUrl,
            string name)
            => MyController<SupplementsController>
                .Instance()
                .WithUser(user => user
                    .InRole(AdministratorRoleName))
                .WithData(data => data
                    .WithEntities(new SupplementCategory()))
                .Calling(c => c.Create(new SupplementFormModel
                {
                    Name = name,
                    CategoryId = 1,
                    Content = content,
                    ImageUrl = imageUrl,
                }))
                .ShouldHave()
                .ActionAttributes(attr => attr
                    .RestrictingForHttpMethod(HttpMethod.Post))
                .AndAlso()
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<SupplementFormModel>()
                    .Passing(data =>
                    {
                        data.Name.ShouldBe(name);
                        data.Content.ShouldBe(content);
                    }));

        [Theory]
        [InlineData("contentTest", "https://addons.cdn.mozilla.net/user-media/previews/full/230/230000.png?modified=1622132702", "amixTest")]
        public void CreateShouldReturnViewWhenCategoryIdIsNotValidAlsoShouldBeAllowedOnlyByPostRequest(
            string content,
            string imageUrl,
            string name)
            => MyController<SupplementsController>
                .Instance()
                .WithUser(user => user
                    .InRole(AdministratorRoleName))
                .Calling(c => c.Create(new SupplementFormModel
                {
                    CategoryId = 1,
                    Name = name,
                    Content = content,
                    ImageUrl = imageUrl,
                    Price = 50,
                    Quantity = 500,
                }))
                .ShouldHave()
                .ActionAttributes(attr => attr
                    .RestrictingForHttpMethod(HttpMethod.Post))
                .AndAlso()
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<SupplementFormModel>()
                    .Passing(data =>
                    {
                        data.Name.ShouldBe(name);
                        data.Content.ShouldBe(content);
                    }));

        [Fact]
        public void EditShouldReturnBadRequestWhenSupplementNotExist()
            => MyController<SupplementsController>
                .Instance()
                .WithUser()
                .Calling(c => c.Edit(1))
                .ShouldReturn()
                .BadRequest();

        [Theory]
        [InlineData("test", "SupplementIdKey")]
        public void EditShouldReturnViewWithSupplementModel(string name, string tempDataKey)
            => MyController<SupplementsController>
                .Instance()
                .WithUser()
                .WithData(data => data
                    .WithEntities(new Supplement
                    {
                        Category = new SupplementCategory(),
                        Name = name,
                    }))
                .Calling(c => c.Edit(1))
                .ShouldHave()
                .TempData(tempData => tempData
                    .ContainingEntryWithKey(tempDataKey))
                .AndAlso()
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<SupplementFormModel>()
                    .Passing(data =>
                    {
                        data.Name.ShouldBe(name);
                    }));

        [Theory]
        [InlineData("nameTest", "contentTest", "https://addons.cdn.mozilla.net/user-media/previews/full/230/230000.png?modified=1622132702")]
        public void EditShouldReturnRedirectToActionWhenModelStateIsValidAlsoShouldBeAllowedOnlyByPostRequestAndAlsoShouldContainsTempData(
            string name,
            string content,
            string imageUrl)
            => MyController<SupplementsController>
                .Instance()
                .WithUser()
                .WithData(data => data
                    .WithEntities(new Supplement
                    {
                        Category = new SupplementCategory(),
                        Name = name,
                    }))
                .Calling(c => c.Edit(1, new SupplementFormModel
                {
                    CategoryId = 1,
                    Name = name,
                    Content = content,
                    ImageUrl = imageUrl,
                    Price = 50,
                    Quantity = 500,
                }))
                .ShouldHave()
                .ActionAttributes(attr => attr
                    .RestrictingForHttpMethod(HttpMethod.Post))
                .AndAlso()
                .ShouldHave()
                .TempData(tempData => tempData
                    .ContainingEntryWithKey(EditKey))
                .AndAlso()
                .ShouldReturn()
                .RedirectToAction("Details", "Supplements");

        [Theory]
        [InlineData("nameTest", "contentTest", "https://addons.cdn.mozilla.net/user-media/previews/full/230/230000.png?modified=1622132702")]
        public void EditShouldReturnBadRequestWhenSupplementNotExistAlsoShouldBeAllowedOnlyByPostRequest(
            string name,
            string content,
            string imageUrl)
            => MyController<SupplementsController>
                .Instance()
                .WithUser()
                .WithData(data => data
                    .WithEntities(new SupplementCategory()))
                .Calling(c => c.Edit(1, new SupplementFormModel
                {
                    CategoryId = 1,
                    Name = name,
                    Content = content,
                    ImageUrl = imageUrl,
                    Price = 50,
                    Quantity = 500,
                }))
                .ShouldHave()
                .ActionAttributes(attr => attr
                    .RestrictingForHttpMethod(HttpMethod.Post))
                .AndAlso()
                .ShouldReturn()
                .BadRequest();

        [Theory]
        [InlineData("nameTest", "contentTest", "https://addons.cdn.mozilla.net/user-media/previews/full/230/230000.png?modified=1622132702")]
        public void EditShouldReturnViewWhenModelStateIsInvalidAlsoShouldBeAllowedOnlyByPostRequest(
            string name,
            string content,
            string imageUrl)
            => MyController<SupplementsController>
                .Instance()
                .WithUser()
                .WithData(data => data
                    .WithEntities(new Supplement
                    {
                        Category = new SupplementCategory(),
                        Name = name,
                    }))
                .Calling(c => c.Edit(1, new SupplementFormModel
                {
                    CategoryId = 1,
                    Name = name,
                    Content = content,
                    ImageUrl = imageUrl,
                }))
                .ShouldHave()
                .ActionAttributes(attr => attr
                    .RestrictingForHttpMethod(HttpMethod.Post))
                .AndAlso()
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<SupplementFormModel>()
                    .Passing(data =>
                    {
                        data.Name.ShouldBe(name);
                        data.Content.ShouldBe(content);
                    }));

        [Fact]
        public void DeleteShouldReturnBadRequestWhenSupplementWithGivenIdNotExist()
            => MyController<SupplementsController>
                .Instance()
                .WithUser()
                .Calling(c => c.Delete(1))
                .ShouldReturn()
                .BadRequest();

        [Fact]
        public void DeleteShouldReturnRedirectToActionWhenSupplementIsDeletedAlsoShouldContainsTempData()
            => MyController<SupplementsController>
                .Instance()
                .WithUser()
                .WithData(data => data
                    .WithEntities(new Supplement
                    {
                        Category = new SupplementCategory(),
                    }))
                .Calling(c => c.Delete(1))
                .ShouldHave()
                .TempData(tempData => tempData
                    .ContainingEntryWithKey(DeleteKey))
                .AndAlso()
                .ShouldReturn()
                .RedirectToAction("All", "Supplements");
    }
}
