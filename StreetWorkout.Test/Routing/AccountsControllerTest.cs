namespace StreetWorkout.Test.Routing
{
    using MyTested.AspNetCore.Mvc;
    using StreetWorkout.Controllers;
    using StreetWorkout.ViewModels.Accounts;
    using Xunit;

    public class AccountsControllerTest
    {
        [Fact]
        public void AccountShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Accounts/Account/test")
                .To<AccountsController>(c => c.Account("test"));

        [Fact]
        public void CompleteAccountShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Accounts/CompleteAccount")
                .To<AccountsController>(c => c.CompleteAccount());

        [Fact]
        public void CompleteAccountShouldBeMappedOnPostMethod()
            => MyRouting
                .Configuration()
                .ShouldMap("/Accounts/CompleteAccount")
                .To<AccountsController>(c => c.CompleteAccount(With.Any<AccountFormModel>()));
    }
}
