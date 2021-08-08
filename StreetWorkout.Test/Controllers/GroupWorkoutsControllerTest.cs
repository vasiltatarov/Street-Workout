namespace StreetWorkout.Test.Controllers
{
    using System;
    using System.Linq;
    using MyTested.AspNetCore.Mvc;
    using Shouldly;
    using Xunit;

    using Data;
    using StreetWorkout.Data.Models.Enums;
    using StreetWorkout.Controllers;
    using StreetWorkout.Data.Models;
    using StreetWorkout.Services.GroupWorkouts.Models;
    using ViewModels.GroupWorkouts;

    public class GroupWorkoutsControllerTest
    {
        [Theory]
        [InlineData("content")]
        public void DetailsShouldReturnViewWithCorrectDetailsModel(string content)
            => MyController<GroupWorkoutsController>
                .Instance(c => c
                    .WithUser()
                    .WithData(data => data
                        .WithEntities(new GroupWorkout
                        {
                            Content = content,
                            Sport = new Sport { Name = "test" },
                            Trainer = new ApplicationUser(),
                        })))
                .Calling(c => c.Details(1))
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<GroupWorkoutDetailsModel>()
                    .Passing(data =>
                    {
                        data.Content.ShouldBe(content);
                        data.Sport.ShouldNotBeNull();
                    }));

        [Fact]
        public void CreateShouldReturnView()
            => MyController<GroupWorkoutsController>
                .Instance()
                .WithUser()
                .Calling(c => c.Create())
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<GroupWorkoutFormModel>());

        [Fact]
        public void CreateShouldBeAllowedOnlyByPostRequestAndShouldReturnUnauthorizedWhenUserIsNotTrainer()
            => MyController<GroupWorkoutsController>
                .Instance()
                .WithUser()
                .Calling(c => c.Create(With.Default<GroupWorkoutFormModel>()))
                .ShouldHave()
                .ActionAttributes(attr => attr
                    .RestrictingForHttpMethod(System.Net.Http.HttpMethod.Post))
                .AndAlso()
                .ShouldReturn()
                .Unauthorized();

        [Fact]
        public void CreateShouldBeAllowedOnlyOnPostMethodAndShouldReturnRedirectionWithValidModelState()
            => MyController<GroupWorkoutsController>
                .Instance()
                .WithUser()
                .WithData(data => data
                    .WithEntities(new ApplicationUser
                    {
                        Id = "TestId",
                        UserRole = UserRole.Trainer,
                    })
                    .WithEntities(new Sport { Name = "test" }))
                .Calling(c => c.Create(new GroupWorkoutFormModel
                {
                    SportId = 1,
                    Address = "AddressAddress",
                    Content = "ContentContent",
                    StartOn = DateTime.Now,
                    EndOn = DateTime.Now.AddDays(2),
                    PricePerPerson = 10,
                    Title = "TitleTitle",
                    MaximumParticipants = 15,
                }))
                .ShouldHave()
                .ActionAttributes(attr => attr
                    .RestrictingForHttpMethod(System.Net.Http.HttpMethod.Post))
                .AndAlso()
                .ShouldReturn()
                .RedirectToAction("All");

        [Fact]
        public void CreateShouldBeAllowedOnlyByPostRequestAndShouldReturnViewModelWhenModelStateIsInvalid()
            => MyController<GroupWorkoutsController>
                .Instance()
                .WithUser()
                .WithData(data => data
                    .WithEntities(new ApplicationUser
                    {
                        Id = "TestId",
                        UserRole = UserRole.Trainer,
                    }))
                .Calling(c => c.Create(With.Default<GroupWorkoutFormModel>()))
                .ShouldHave()
                .ActionAttributes(attr => attr
                    .RestrictingForHttpMethod(System.Net.Http.HttpMethod.Post))
                .AndAlso()
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<GroupWorkoutFormModel>());

        [Fact]
        public void AllShouldReturnViewWithEmptyGroupWorkoutsCollection()
            => MyController<GroupWorkoutsController>
                .Instance()
                .WithUser()
                .Calling(c => c.All(With.Default<GroupWorkoutsQueryModel>()))
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<GroupWorkoutsQueryModel>()
                    .Passing(data =>
                    {
                        data.GroupWorkouts.ShouldBeEmpty();
                    }));

        [Fact]
        public void AllShouldReturnViewWithCorrectCountGroupWorkoutsCollection()
            => MyController<GroupWorkoutsController>
                .Instance()
                .WithUser()
                .WithData(GroupWorkouts.TenGroupWorkouts())
                .Calling(c => c.All(With.Default<GroupWorkoutsQueryModel>()))
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<GroupWorkoutsQueryModel>()
                    .Passing(data =>
                    {
                        data.GroupWorkouts.Count().ShouldBe(9);
                        data.TotalGroupWorkouts.ShouldBe(10);
                    }));
    }
}
