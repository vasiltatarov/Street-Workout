namespace StreetWorkout.Test.Controllers
{
    using System.Linq;
    using MyTested.AspNetCore.Mvc;
    using Shouldly;
    using StreetWorkout.Controllers;
    using StreetWorkout.Data.Models;
    using StreetWorkout.Services.Workouts.Models;
    using StreetWorkout.ViewModels.Workouts;
    using Xunit;

    public class WorkoutsControllerTest
    {
        [Fact]
        public void AllShouldReturnViewWithAllWorkouts()
            => MyController<WorkoutsController>
                .Instance(controller => controller
                    .WithUser()
                    .WithData(data => data
                        .WithEntities(new ApplicationUser
                        {
                            Id = "TestId",
                        })
                        .WithEntities(Enumerable
                                .Range(1, 10)
                                .Select(x => new Workout()))))
                .Calling(c => c.All(With.Default<WorkoutsQueryModel>()))
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<WorkoutsQueryModel>()
                    .Passing(data =>
                    {
                        data.ShouldNotBeNull();
                        data.CurrentPage.ShouldBe(1);
                        data.TotalWorkouts.ShouldBe(10);
                    }));

        [Fact]
        public void AllShouldReturnViewWithoutWorkoutsWhenDatabaseIsEmpty()
            => MyController<WorkoutsController>
                .Instance(controller => controller
                    .WithUser()
                    .WithData(data => data
                        .WithEntities(new ApplicationUser
                        {
                            Id = "TestId",
                        })))
                .Calling(c => c.All(With.Default<WorkoutsQueryModel>()))
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<WorkoutsQueryModel>()
                    .Passing(data =>
                    {
                        data.ShouldNotBeNull();
                        data.CurrentPage.ShouldBe(1);
                        data.TotalWorkouts.ShouldBe(0);
                    }));

        [Theory]
        [InlineData("TestId", "title", 1, "sportTest")]
        public void DetailsShouldReturnViewWithWorkoutDetailsServiceModel(string userId, string title, int workoutId, string information)
            => MyController<WorkoutsController>
                .Instance(controller => controller
                    .WithUser()
                    .WithData(data => data
                        .WithEntities(new ApplicationUser
                        {
                            Id = userId,
                        })
                        .WithEntities(new Workout
                        {
                            Title = title,
                            UserId = userId,
                            Sport = new Sport { Name = information },
                            BodyPart = new BodyPart(),
                        })))
                .Calling(c => c.Details(workoutId, information))
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<WorkoutDetailsServiceModel>()
                    .Passing(data =>
                    {
                        data.ShouldNotBeNull();
                        data.Title.ShouldBe(title);
                    }));

        [Theory]
        [InlineData("TestId", 1)]
        public void DetailsShouldReturnBadRequestWhenWorkoutWithGivenIdNotExistInDatabase(string userId, int workoutId)
            => MyController<WorkoutsController>
                .Instance(controller => controller
                    .WithUser()
                    .WithData(data => data
                        .WithEntities(new ApplicationUser
                        {
                            Id = userId,
                        })))
                .Calling(c => c.Details(workoutId, string.Empty))
                .ShouldReturn()
                .BadRequest();

        [Fact]
        public void CreateShouldReturnViewWithWorkoutFormModel()
            => MyController<WorkoutsController>
                .Instance(controller => controller
                    .WithUser()
                    .WithData(data => data
                        .WithEntities(new ApplicationUser
                        {
                            Id = "TestId",
                        })))
                .Calling(c => c.Create())
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<WorkoutFormModel>()
                    .ShouldNotBeNull());

        [Fact]
        public void CreateShouldReturnRedirectToActionWhenModelStateIsValidAndShouldBeAllowedOnlyByPostRequest()
            => MyController<WorkoutsController>
                .Instance(controller => controller
                    .WithUser()
                    .WithData(data => data
                        .WithEntities(new ApplicationUser
                        {
                            Id = "TestId",
                        })
                        .WithEntities(new Sport())
                        .WithEntities(new BodyPart())))
                .Calling(c => c.Create(new WorkoutFormModel
                {
                    DifficultLevel = 1,
                    SportId = 1,
                    BodyPartId = 1,
                    Minutes = 12,
                    Content = "adsdasasasd",
                    Title = "dfasfaxcvfdvdxs",
                }))
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForHttpMethod(HttpMethod.Post))
                .AndAlso()
                .ShouldReturn()
                .RedirectToAction("All");

        [Fact]
        public void CreateShouldReturnViewWithTheSameModelWhenModelStateIsInvalidAndShouldBeAllowedOnlyByPostRequest()
            => MyController<WorkoutsController>
                .Instance(controller => controller
                    .WithUser()
                    .WithData(data => data
                        .WithEntities(new ApplicationUser
                        {
                            Id = "TestId",
                        })
                        .WithEntities(new Sport())
                        .WithEntities(new BodyPart())))
                .Calling(c => c.Create(new WorkoutFormModel
                {
                    DifficultLevel = 1,
                    SportId = 1,
                    BodyPartId = 1,
                    Minutes = 12,
                    Title = "dfasfaxcvfdvdxs",
                }))
                .ShouldHave()
                .InvalidModelState()
                .ActionAttributes(attributes => attributes
                    .RestrictingForHttpMethod(HttpMethod.Post))
                .AndAlso()
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<WorkoutFormModel>()
                    .Passing(data =>
                    {
                        data.ShouldNotBeNull();
                        data.DifficultLevel.ShouldBe(1);
                        data.Content.ShouldBeNull();
                        data.Title.ShouldNotBeNull();
                        data.Sports.Count().ShouldBe(1);
                    }));
    }
}
