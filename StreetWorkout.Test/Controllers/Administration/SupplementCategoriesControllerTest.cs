using Shouldly;

namespace StreetWorkout.Test.Controllers.Administration
{
    using System.Collections.Generic;
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    using StreetWorkout.Areas.Administration.Controllers;
    using StreetWorkout.Services.SupplementCategories.Models;
    using StreetWorkout.Data.Models;

    using static WebConstants.TempDataMessageKeys;

    public class SupplementCategoriesControllerTest
    {
        [Fact]
        public void AllShouldReturnViewWithCorrectModel()
            => MyController<SupplementCategoriesController>
                .Instance()
                .WithUser()
                .Calling(c => c.All())
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<IEnumerable<SupplementCategoryServiceModel>>());

        [Fact]
        public void DeleteShouldReturnBadRequestWhenEntityWithGivenIdNotExist()
            => MyController<SupplementCategoriesController>
                .Instance()
                .WithUser()
                .Calling(c => c.Delete(1))
                .ShouldReturn()
                .BadRequest();

        [Fact]
        public void DeleteShouldReturnRedirectToActionWhenEntityIsDeletedSuccessfullyAndAlsoShouldContainsTempData()
            => MyController<SupplementCategoriesController>
                .Instance()
                .WithUser()
                .WithData(data => data
                    .WithEntities(new SupplementCategory()))
                .Calling(c => c.Delete(1))
                .ShouldHave()
                .TempData(tempData => tempData
                    .ContainingEntryWithKey(DeleteKey))
                .AndAlso()
                .ShouldReturn()
                .RedirectToAction("All");

        [Fact]
        public void RestoreShouldReturnBadRequestWhenEntityWithGivenIdNotExist()
            => MyController<SupplementCategoriesController>
                .Instance()
                .WithUser()
                .Calling(c => c.Restore(1))
                .ShouldReturn()
                .BadRequest();

        [Fact]
        public void RestoreShouldReturnRedirectToActionWhenEntityIsRestoredSuccessfullyAndAlsoShouldContainsTempData()
            => MyController<SupplementCategoriesController>
                .Instance()
                .WithUser()
                .WithData(data => data
                    .WithEntities(new SupplementCategory()))
                .Calling(c => c.Restore(1))
                .ShouldHave()
                .TempData(tempData => tempData
                    .ContainingEntryWithKey(RestoreKey))
                .AndAlso()
                .ShouldReturn()
                .RedirectToAction("All");

        [Fact]
        public void EditShouldReturnViewWithCorrectModel()
            => MyController<SupplementCategoriesController>
                .Instance()
                .WithUser()
                .WithData(data => data
                    .WithEntities(new SupplementCategory()))
                .Calling(c => c.Edit(1))
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<SupplementCategoryEditServiceModel>());

        [Fact]
        public void EditShouldReturnViewWhenModelStateIsInvalidAlsoShouldBeAllowedOnlyByPostRequest()
            => MyController<SupplementCategoriesController>
                .Instance()
                .WithUser()
                .Calling(c => c.Edit(1, With.Default<SupplementCategoryEditServiceModel>()))
                .ShouldHave()
                .ActionAttributes(attr => attr
                    .RestrictingForHttpMethod(HttpMethod.Post))
                .AndAlso()
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<SupplementCategoryEditServiceModel>()
                    .Passing(data =>
                    {
                        data.Name.ShouldBeNull();
                    }));

        [Fact]
        public void EditShouldReturnBadRequestWhenModelStateIsValidAndEntityWithGivenIdNotExistAlsoShouldBeAllowedOnlyByPostRequest()
            => MyController<SupplementCategoriesController>
                .Instance()
                .WithUser()
                .Calling(c => c.Edit(1, new SupplementCategoryEditServiceModel
                {
                    Name = "test"
                }))
                .ShouldHave()
                .ActionAttributes(attr => attr
                    .RestrictingForHttpMethod(HttpMethod.Post))
                .AndAlso()
                .ShouldReturn()
                .BadRequest();

        [Fact]
        public void EditShouldReturnRedirectToActionWhenModelStateIsValidAlsoShouldBeAllowedOnlyByPostRequest()
            => MyController<SupplementCategoriesController>
                .Instance()
                .WithUser()
                .WithData(data => data
                    .WithEntities(new SupplementCategory()))
                .Calling(c => c.Edit(1, new SupplementCategoryEditServiceModel
                {
                    Name = "test"
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
                .RedirectToAction("All");
    }
}
