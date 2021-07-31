namespace StreetWorkout.Test.Controllers
{
    using System.Linq;
    using System.Collections.Generic;
    using Xunit;
    using MyTested.AspNetCore.Mvc;
    using Data.Models;
    using Data.Models.Enums;
    using StreetWorkout.Controllers;
    using StreetWorkout.Services.Homes.Models;
    using ViewModels;

    public class HomeControllerTest
    {
        [Fact]
        public void PrivacyShouldReturnView()
            => MyController<HomeController>
                .Calling(x => x.Privacy())
                .ShouldReturn()
                .View();

        [Fact]
        public void ErrorShouldReturnViewWithErrorViewModel()
            => MyController<HomeController>
                .Calling(x => x.Error())
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<ErrorViewModel>());

        [Fact]
        public void IndexShouldReturnViewWithCorrectModelAndData()
            => MyController<HomeController>
                .Instance(controller => controller
                    .WithUser(user => user
                        .WithIdentifier("sv"))
                    .WithData(data => data
                        .WithEntities(new ApplicationUser
                        {
                            Id = "sv",
                            UserName = "vasko",
                            UserRole = UserRole.Trainer
                        })
                        .WithEntities(GetWorkouts())))
                .Calling(x => x.Index())
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<IndexServiceModel>()
                    .Passing(model =>
                    {
                        Assert.False(model.IsAccountCompleted);
                        Assert.True(model.IsTrainer);
                        Assert.Equal(3, model.Workouts.Count());
                        Assert.Empty(model.Users);
                    }));

        private static IEnumerable<Workout> GetWorkouts()
            => Enumerable.Range(1, 4)
                .Select(x => new Workout
                {
                    BodyPart = new BodyPart(),
                    Sport = new Sport(),
                });
    }
}
