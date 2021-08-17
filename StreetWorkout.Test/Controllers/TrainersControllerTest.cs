﻿namespace StreetWorkout.Test.Controllers
{
    using Xunit;
    using Shouldly;
    using MyTested.AspNetCore.Mvc;
    using StreetWorkout.Controllers;
    using StreetWorkout.Data.Models;
    using StreetWorkout.Data.Models.Enums;
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
                .Calling(c => c.All(With.Default<AllUsersQueryModel>()))
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<AllUsersQueryModel>()
                    .Passing(data =>
                    {
                        data.ShouldNotBeNull();
                        data.CurrentPage.ShouldBe(1);
                        data.TotalUsers.ShouldBe(1);
                        data.TotalUsers.ShouldBe(1);
                    }));

        [Fact]
        public void AllShouldReturnViewWithoutTrainersWhenDatabaseIsEmpty()
            => MyController<TrainersController>
                .Instance()
                .Calling(c => c.All(With.Default<AllUsersQueryModel>()))
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<AllUsersQueryModel>()
                    .Passing(data =>
                    {
                        data.ShouldNotBeNull();
                        data.CurrentPage.ShouldBe(1);
                        data.TotalUsers.ShouldBe(0);
                        data.TotalUsers.ShouldBe(0);
                    }));
    }
}
