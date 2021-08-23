namespace StreetWorkout.Test.Controllers.Administration
{
    using System;
    using MyTested.AspNetCore.Mvc;
    using StreetWorkout.Areas.Administration.Controllers;
    using StreetWorkout.Data.Models;
    using StreetWorkout.Data.Models.Enums;
    using StreetWorkout.ViewModels.GroupWorkouts;
    using Xunit;
    using static WebConstants;

    public class GroupWorkoutsControllerTest
    {
        [Fact]
        public void EditShouldReturnUnauthorizedWhenUserIsNotAdministratorOrGroupWorkoutOwner()
            => MyController<GroupWorkoutsController>
                .Instance()
                .WithUser()
                .Calling(c => c.Edit(1))
                .ShouldReturn()
                .Unauthorized();

        [Fact]
        public void EditShouldReturnViewWithCorrectModel()
            => MyController<GroupWorkoutsController>
                .Instance()
                .WithUser(user => user
                    .InRole(AdministratorRoleName))
                .WithData(data => data
                    .WithEntities(new GroupWorkout()))
                .Calling(c => c.Edit(1))
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<GroupWorkoutFormModel>());

        [Fact]
        public void EditShouldReturnUnauthorizedWhenUserIsNotAdministratorOrGroupWorkoutOwnerAlsoShouldBeAllowedOnlyByPostRequest()
            => MyController<GroupWorkoutsController>
                .Instance()
                .WithUser()
                .Calling(c => c.Edit(With.Default<GroupWorkoutFormModel>()))
                .ShouldHave()
                .ActionAttributes(attr => attr
                    .RestrictingForHttpMethod(HttpMethod.Post))
                .AndAlso()
                .ShouldReturn()
                .Unauthorized();

        [Fact]
        public void EditShouldReturnUnauthorizedWhenUserIsNotTrainerAlsoShouldBeAllowedOnlyByPostRequest()
            => MyController<GroupWorkoutsController>
                .Instance()
                .WithUser(user => user
                    .InRole(AdministratorRoleName))
                .Calling(c => c.Edit(With.Default<GroupWorkoutFormModel>()))
                .ShouldHave()
                .ActionAttributes(attr => attr
                    .RestrictingForHttpMethod(HttpMethod.Post))
                .AndAlso()
                .ShouldReturn()
                .Unauthorized();

        [Theory]
        [InlineData("titleTitle", "address here", "missing content", "TestId")]
        public void EditShouldReturnViewWhenModelStateIsInvalidAlsoShouldBeAllowedOnlyByPostRequest(
            string title,
            string address,
            string content,
            string userId)
            => MyController<GroupWorkoutsController>
                .Instance()
                .WithUser(user => user
                    .InRole(AdministratorRoleName))
                .WithData(data => data
                    .WithEntities(new GroupWorkout
                    {
                        Sport = new Sport(),
                        Trainer = new ApplicationUser
                        {
                            Id = userId,
                            Country = new Country(),
                            UserRole = UserRole.Trainer,
                        },
                    }))
                .Calling(c => c.Edit(new GroupWorkoutFormModel
                {
                    Title = title,
                    Address = address,
                    SportId = 1,
                    Content = content,
                }))
                .ShouldHave()
                .ActionAttributes(attr => attr
                    .RestrictingForHttpMethod(HttpMethod.Post))
                .AndAlso()
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<GroupWorkoutFormModel>());

        [Theory]
        [InlineData("titleTitle", "address here", "missing content", "TestId")]
        public void EditShouldReturnRedirectToActionWhenModelStateIsValidAlsoShouldBeAllowedOnlyByPostRequest(
            string title,
            string address,
            string content,
            string userId)
            => MyController<GroupWorkoutsController>
                .Instance()
                .WithUser(user => user
                    .InRole(AdministratorRoleName))
                .WithData(data => data
                    .WithEntities(new GroupWorkout
                    {
                        Sport = new Sport(),
                        Trainer = new ApplicationUser
                        {
                            Id = userId,
                            Country = new Country(),
                            UserRole = UserRole.Trainer,
                        },
                    }))
                .Calling(c => c.Edit(new GroupWorkoutFormModel
                {
                    Title = title,
                    Address = address,
                    SportId = 1,
                    StartOn = DateTime.UtcNow.AddDays(1),
                    EndOn = DateTime.UtcNow.AddDays(2),
                    MaximumParticipants = 30,
                    PricePerPerson = 15,
                    Content = content,
                }))
                .ShouldHave()
                .ActionAttributes(attr => attr
                    .RestrictingForHttpMethod(HttpMethod.Post))
                .AndAlso()
                .ShouldReturn()
                .RedirectToAction("Details", "GroupWorkouts");

        [Fact]
        public void DeleteShouldReturnUnauthorizedWhenUserIsNotAdministratorOrGroupWorkoutOwner()
            => MyController<GroupWorkoutsController>
                .Instance()
                .WithUser()
                .Calling(c => c.Delete(1))
                .ShouldReturn()
                .Unauthorized();

        [Fact]
        public void DeleteShouldReturnBadRequestWhenUserIsAdministratorButEntityWithGivenIdNotExist()
            => MyController<GroupWorkoutsController>
                .Instance()
                .WithUser(user => user
                    .InRole(AdministratorRoleName))
                .Calling(c => c.Delete(1))
                .ShouldReturn()
                .BadRequest();

        [Fact]
        public void DeleteShouldReturnRedirectToActionWhenEntityIsDeletedSuccessfully()
            => MyController<GroupWorkoutsController>
                .Instance()
                .WithUser(user => user
                    .InRole(AdministratorRoleName))
                .WithData(data => data
                    .WithEntities(new GroupWorkout()))
                .Calling(c => c.Delete(1))
                .ShouldReturn()
                .RedirectToAction("All", "GroupWorkouts");
    }
}
