namespace StreetWorkout.Test.Routing.Administration
{
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    using StreetWorkout.Areas.Administration.Controllers;
    using StreetWorkout.Services.SupplementCategories.Models;

    public class SupplementCategoriesControllerTest
    {
        [Fact]
        public void AllShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Administration/SupplementCategories/All")
                .To<SupplementCategoriesController>(c => c.All());

        [Fact]
        public void DeleteShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Administration/SupplementCategories/Delete/1")
                .To<SupplementCategoriesController>(c => c.Delete(1));

        [Fact]
        public void RestoreShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Administration/SupplementCategories/Restore/1")
                .To<SupplementCategoriesController>(c => c.Restore(1));

        [Fact]
        public void EditShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Administration/SupplementCategories/Edit/1")
                .To<SupplementCategoriesController>(c => c.Edit(1));

        [Fact]
        public void EditShouldBeMappedAndShouldBeAllowedOnlyByPostRequest()
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithPath("/Administration/SupplementCategories/Edit/1")
                    .WithMethod(HttpMethod.Post))
                .To<SupplementCategoriesController>(c => c.Edit(1, With.Any<SupplementCategoryEditServiceModel>()));
    }
}
