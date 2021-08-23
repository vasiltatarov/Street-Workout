namespace StreetWorkout.Test.Routing.Administration
{
    using MyTested.AspNetCore.Mvc;
    using StreetWorkout.Areas.Administration.Controllers;
    using StreetWorkout.ViewModels.Supplements;
    using Xunit;

    public class SupplementsControllerTest
    {
        [Fact]
        public void CreateShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Administration/Supplements/Create")
                .To<SupplementsController>(c => c.Create());

        [Fact]
        public void CreateShouldBeMappedAndShouldBeAllowedOnlyByPostRequest()
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithPath("/Administration/Supplements/Create")
                    .WithMethod(HttpMethod.Post))
                .To<SupplementsController>(c => c.Create(With.Any<SupplementFormModel>()));

        [Fact]
        public void EditShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Administration/Supplements/Edit/1")
                .To<SupplementsController>(c => c.Edit(1));

        [Fact]
        public void EditShouldBeMappedAndShouldBeAllowedOnlyByPostRequest()
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithPath("/Administration/Supplements/Edit/1")
                    .WithMethod(HttpMethod.Post))
                .To<SupplementsController>(c => c.Edit(1, With.Any<SupplementFormModel>()));

        [Fact]
        public void DeleteShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Administration/Supplements/Delete/1")
                .To<SupplementsController>(c => c.Delete(1));

    }
}
