namespace StreetWorkout.Test.Controllers
{
    using Xunit;
    using Shouldly;
    using MyTested.AspNetCore.Mvc;
    using StreetWorkout.Controllers;
    using Data.Models;
    using Data.Models.Enums;
    using ViewModels.Trainers;

    public class TrainersControllerTest
    {
        [Fact]
        public void AllShouldReturnViewWithAllTrainersViewModel()
            => MyController<TrainersController>
                .Instance()
                .WithData(data => data
                    .WithEntities(new UserData
                    {
                        Sport = new Sport(),
                        Goal = new Goal(),
                        TrainingFrequency = new TrainingFrequency(),
                        User = new ApplicationUser
                        {
                            UserName = "test",
                            UserRole = UserRole.Trainer,
                        },
                    }))
                .Calling(c => c.All(With.Default<AllTrainersViewModel>()))
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<AllTrainersViewModel>()
                    .Passing(data =>
                    {
                        data.ShouldNotBeNull();
                        data.CurrentPage.ShouldBe(1);
                        data.TotalTrainers.ShouldBe(1);
                        data.TotalTrainers.ShouldBe(1);
                    }));

        [Fact]
        public void AllShouldReturnViewWithoutTrainersWhenDatabaseIsEmpty()
            => MyController<TrainersController>
                .Instance()
                .Calling(c => c.All(With.Default<AllTrainersViewModel>()))
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<AllTrainersViewModel>()
                    .Passing(data =>
                    {
                        data.ShouldNotBeNull();
                        data.CurrentPage.ShouldBe(1);
                        data.TotalTrainers.ShouldBe(0);
                        data.TotalTrainers.ShouldBe(0);
                    }));
    }
}
