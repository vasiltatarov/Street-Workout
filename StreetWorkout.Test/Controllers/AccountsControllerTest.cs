namespace StreetWorkout.Test.Controllers
{
    using System.Linq;
    using System.Collections.Generic;
    using Xunit;
    using MyTested.AspNetCore.Mvc;

    using Data.Models;
    using Data.Models.Enums;
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
                        Assert.NotNull(data);
                        Assert.Null(data.Description);
                        Assert.Equal(0, data.GoalId);
                        Assert.Equal(0, data.SportId);
                        Assert.Equal(0, data.Weight);
                        Assert.Equal(0, data.Height);
                        Assert.Equal(2, data.Sports.Count());
                    }));
    }
}
