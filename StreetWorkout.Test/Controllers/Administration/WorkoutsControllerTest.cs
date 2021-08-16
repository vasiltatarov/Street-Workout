namespace StreetWorkout.Test.Controllers.Administration
{
    using MyTested.AspNetCore.Mvc;
    using Shouldly;
    using Xunit;

    using StreetWorkout.Areas.Administration.Controllers;
    using StreetWorkout.Data.Models;
    using StreetWorkout.Data.Models.Enums;
    using ViewModels.Workouts;
    using Infrastructure;

    using static WebConstants;
    using static WebConstants.TempDataMessageKeys;

    public class WorkoutsControllerTest
    {
        [Fact]
        public void EditShouldReturnViewWhenAdministratorTryToEditWorkout()
            => MyController<WorkoutsController>
                .Instance()
                .WithUser(user => user
                    .InRole(AdministratorRoleName))
                .WithData(data => data
                    .WithEntities(new Workout
                    {
                        Sport = new Sport(),
                        BodyPart = new BodyPart(),
                        Content = "content",
                        UserId = "TestId",
                    }))
                .Calling(c => c.Edit(1))
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<WorkoutFormModel>()
                    .Passing(data =>
                    {
                        data.Content.ShouldBe("content");
                    }));

        [Fact]
        public void EditShouldReturnViewWhenWorkoutOwnerTryToEditWorkout()
            => MyController<WorkoutsController>
                .Instance()
                .WithUser()
                .WithData(data => data
                    .WithEntities(new Workout
                    {
                        Sport = new Sport(),
                        BodyPart = new BodyPart(),
                        Content = "content",
                        UserId = "TestId",
                    }))
                .Calling(c => c.Edit(1))
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<WorkoutFormModel>()
                    .Passing(data =>
                    {
                        data.Content.ShouldBe("content");
                    }));

        [Fact]
        public void EditShouldReturnUnauthorizedWhenUserIsNotAdministratorOrWorkoutOwner()
            => MyController<WorkoutsController>
                .Instance()
                .WithUser()
                .WithData(data => data
                    .WithEntities(new Workout
                    {
                        Sport = new Sport(),
                        BodyPart = new BodyPart(),
                        UserId = "notExist",
                    }))
                .Calling(c => c.Edit(1))
                .ShouldReturn()
                .Unauthorized();

        [Fact]
        public void EditShouldReturnBadRequest()
            => MyController<WorkoutsController>
                .Instance()
                .WithUser(user => user
                    .InRole(AdministratorRoleName))
                .Calling(c => c.Edit(1))
                .ShouldReturn()
                .BadRequest();

        [Theory]
        [InlineData("titleTitle", "content", "TestId", "Details")]
        public void EditShouldRedirectToActionWhenModelStateIsValidAndUserIsAdministratorAndShouldBeAllowedOnlyByPostRequest(
            string title,
            string content,
            string userId,
            string detailsAction)
            => MyController<WorkoutsController>
                .Instance()
                .WithUser(user => user
                      .InRole(AdministratorRoleName))
                .WithData(data => data
                      .WithEntities(new Workout
                      {
                          Sport = new Sport(),
                          BodyPart = new BodyPart(),
                          Title = title,
                          Content = content,
                          DifficultLevel = DifficultLevel.Advanced,
                          Minutes = 10,
                          UserId = userId,
                      }))
                .Calling(c => c.Edit(new WorkoutFormModel
                {
                    DifficultLevel = 1,
                    Minutes = 15,
                    Title = title,
                    Content = content,
                    SportId = 1,
                    BodyPartId = 1,
                }))
                .ShouldHave()
                .ActionAttributes(attr => attr
                    .RestrictingForHttpMethod(HttpMethod.Post))
                .AndAlso()
                .ShouldHave()
                .TempData(data => data
                    .ContainingEntryWithKey(EditKey))
                .AndAlso()
                .ShouldReturn()
                .RedirectToAction(detailsAction,
                    typeof(WorkoutsController).GetControllerName());

        [Fact]
        public void
            EditShouldReturnUnauthorizedWhenUserIsNotAdministratorOrWorkoutOwnerAlsoShouldBeAllowedOnlyBePostRequest()
            => MyController<WorkoutsController>
                .Instance()
                .WithUser()
                .WithData(data => data
                    .WithEntities(new Workout
                    {
                        Sport = new Sport(),
                        BodyPart = new BodyPart(),
                        UserId = "notExist",
                    }))
                .Calling(c => c.Edit(With.Default<WorkoutFormModel>()))
                .ShouldHave()
                .ActionAttributes(attr => attr
                    .RestrictingForHttpMethod(HttpMethod.Post))
                .AndAlso()
                .ShouldReturn()
                .Unauthorized();

        [Theory]
        [InlineData("titleTitle", "content")]
        public void EditShouldReturnViewWhenModelStateIsInvalidAndUserIsAdministratorAndAlsoShouldBeAllowedOnlyByPostRequest(
            string title,
            string content)
            => MyController<WorkoutsController>
                .Instance()
                .WithUser(user => user
                    .InRole(AdministratorRoleName))
                .WithData(data => data
                    .WithEntities(new Workout
                    {
                        Sport = new Sport(),
                        BodyPart = new BodyPart(),
                        Title = title,
                        Content = content,
                    }))
                .Calling(c => c.Edit(new WorkoutFormModel
                {
                    Title = title,
                    Content = content,
                }))
                .ShouldHave()
                .ActionAttributes(attr => attr
                    .RestrictingForHttpMethod(HttpMethod.Post))
                .AndAlso()
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<WorkoutFormModel>()
                    .Passing(data =>
                    {
                        data.Title.ShouldBe(title);
                        data.Content.ShouldBe(content);
                    }));

        [Fact]
        public void DeleteShouldReturnUnauthorizedWhenUserIsNotAdministratorOrWorkoutOwner()
            => MyController<WorkoutsController>
                .Instance()
                .WithUser()
                .WithData(data => data
                    .WithEntities(new Workout
                    {
                        Sport = new Sport(),
                        BodyPart = new BodyPart(),
                    }))
                .Calling(c => c.Delete(1))
                .ShouldReturn()
                .Unauthorized();

        [Fact]
        public void DeleteShouldReturnBadRequestWhenWorkoutNotExist()
            => MyController<WorkoutsController>
                .Instance()
                .WithUser(user => user
                    .InRole(AdministratorRoleName))
                .Calling(c => c.Delete(1))
                .ShouldReturn()
                .BadRequest();

        [Fact]
        public void DeleteShouldRedirectToActionWhenUserIsAdministratorAndWorkoutExist()
            => MyController<WorkoutsController>
                .Instance()
                .WithUser(user => user
                    .InRole(AdministratorRoleName))
                .WithData(data => data
                    .WithEntities(new Workout
                    {
                        Sport = new Sport(),
                        BodyPart = new BodyPart(),
                    }))
                .Calling(c => c.Delete(1))
                .ShouldReturn()
                .RedirectToAction("All", "Workouts");
    }
}
