namespace StreetWorkout.Test.Controllers
{
    using System.Linq;
    using System.Collections.Generic;
    using Shouldly;
    using Xunit;
    using MyTested.AspNetCore.Mvc;

    using StreetWorkout.Data.Models;
    using StreetWorkout.Data.Models.Enums;
    using StreetWorkout.Controllers;
    using ViewModels.Accounts;

    public class AccountsControllerTest
    {
        [Fact]
        public void AccountShouldReturnViewWithCorrectAccountViewModel()
            => MyController<AccountsController>
                .Instance(controller => controller
                    .WithUser()
                    .WithData(data => data
                        .WithEntities(new ApplicationUser
                        {
                            UserName = "vasko",
                            Country = new Country(),
                            City = "Chakalo",
                            ImageUrl = "test.png"
                        })))
                .Calling(c => c.Account("vasko"))
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<AccountViewModel>()
                    .Passing(data =>
                    {
                        Assert.NotNull(data);
                        Assert.Equal("vasko", data.Username);
                        Assert.Equal("Chakalo", data.City);
                        Assert.Equal("test.png", data.ImageUrl);
                    }));

        [Fact]
        public void CompleteAccountShouldReturnCorrectViewWithPopulatedDataFromDatabase()
            => MyController<AccountsController>
                .Instance(controller => controller
                    .WithUser(user => user
                        .WithIdentifier("sv1"))
                    .WithData(data => data
                        .WithEntities(new ApplicationUser
                        {
                            Id = "sv1",
                            UserRole = UserRole.Trainer,
                        })
                        .WithEntities(new List<Sport>
                        {
                            new (){ Name = "a" },
                            new (){ Name = "b" }
                        })
                        .WithEntities(new Goal { Name = "sads" })))
                .Calling(c => c.CompleteAccount())
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<AccountFormModel>()
                    .Passing(data =>
                    {
                        Assert.NotNull(data);
                        Assert.Equal(2, data.Sports.Count());
                        Assert.Single(data.Goals);
                    }));

        [Fact]
        public void CompleteAccountShouldBeAllowedOnlyForPostRequestAndAuthorizedUsers()
            => MyController<AccountsController>
                .Instance(controller => controller
                    .WithUser(user => user
                        .WithIdentifier("vs1"))
                    .WithData(data => data
                        .WithEntities(new ApplicationUser
                        {
                            Id = "vs1"
                        })))
                .Calling(c => c.CompleteAccount(With.Default<AccountFormModel>()))
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForHttpMethod(HttpMethod.Post));

        [Fact]
        public void CompleteAccountShouldReturnViewWithTheSameModelWhenModelStateIsInvalid()
            => MyController<AccountsController>
                .Instance(controller => controller
                    .WithUser(user => user
                        .WithIdentifier("vs"))
                    .WithData(data => data
                        .WithEntities(new ApplicationUser
                        {
                            Id = "vs"
                        })
                        .WithEntities(new List<Sport>
                        {
                            new () { Name = "q" },
                            new () { Name = "s" },
                        })))
                .Calling(c => c.CompleteAccount(With.Default<AccountFormModel>()))
                .ShouldHave()
                .InvalidModelState()
                .AndAlso()
                .ShouldReturn()
                .View(result => result
                    .WithModelOfType<AccountFormModel>()
                    .Passing(data =>
                    {
                        data.ShouldNotBeNull();
                        data.Description.ShouldBeNull();
                        data.GoalId.ShouldBe(0);
                        data.SportId.ShouldBe(0);
                        data.Weight.ShouldBe(0);
                        data.Height.ShouldBe(0);
                        data.Sports.Count().ShouldBe(2);
                    }));

        [Fact]
        public void CompleteAccountShouldReturnRedirectToActionAfterCompleteAccountCorrectAndShouldBeAllowedOnlyForPostRequest()
            => MyController<AccountsController>
                .Instance(controller => controller
                    .WithUser(user => user
                        .WithIdentifier("vs"))
                    .WithData(data => data
                        .WithEntities(new ApplicationUser
                        {
                            Id = "vs"
                        })
                        .WithEntities(new Goal
                        {
                            Name = "as"
                        })
                        .WithEntities(new Sport
                        {
                            Name = "ads"
                        })
                        .WithEntities(new TrainingFrequency()
                        {
                            Name = "ads"
                        })))
                .Calling(c => c.CompleteAccount(new AccountFormModel
                {
                    GoalId = 1,
                    SportId = 1,
                    TrainingFrequencyId = 1,
                    Description = "asdadasasddasasdsd",
                    Height = 123,
                    Weight = 56,
                }))
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForHttpMethod(System.Net.Http.HttpMethod.Post))
                .AndAlso()
                .ShouldReturn()
                .RedirectToAction("Index", "Home");

        [Fact]
        public void CompleteAccountShouldRedirectToActionAfterTryToCompleteAccountWhichIsAlreadyCompletedAndShouldBeAllowedOnlyForPostRequest()
            => MyController<AccountsController>
                .Instance(controller => controller
                    .WithUser()
                    .WithData(data => data
                        .WithEntities(new ApplicationUser
                        {
                            Id = "TestId",
                            IsAccountCompleted = true,
                        })))
                .Calling(c => c.CompleteAccount(With.Default<AccountFormModel>()))
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForHttpMethod(System.Net.Http.HttpMethod.Post))
                .AndAlso()
                .ShouldReturn()
                .RedirectToAction("Index", "Home");
    }
}
