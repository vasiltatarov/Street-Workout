namespace StreetWorkout.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using Services;
    using Infrastructure;
    using ViewModels.Accounts;

    [Authorize]
    public class AccountsController : Controller
    {
        private readonly IAccountService accountService;

        public AccountsController(IAccountService accountService)
            => this.accountService = accountService;

        public IActionResult CompleteAccount()
            => this.IsAccountComplete()
                ? this.RedirectToAction("Index", "Home")
                : this.View(new AccountFormModel
                {
                    Sports = this.accountService.GetSportsInAccountFormModel(),
                    Goals = this.accountService.GetGoalsInAccountFormModel(),
                    TrainingFrequencies = this.accountService.GetTrainingFrequenciesInAccountFormModel(),
                });

        [HttpPost]
        public IActionResult CompleteAccount(AccountFormModel data)
        {
            var userId = this.User.GetId();

            if (this.IsAccountComplete() || this.accountService.IsUserDataExists(userId))
            {
                return this.RedirectToAction("Index", "Home");
            }

            if (!this.accountService.IsValidSportId(data.SportId))
            {
                this.ModelState.AddModelError(nameof(data.SportId), "Invalid Sport.");
            }

            if (!this.accountService.IsValidGoalId(data.GoalId))
            {
                this.ModelState.AddModelError(nameof(data.GoalId), "Invalid Goal.");
            }

            if (!this.accountService.IsValidTrainingFrequencyId(data.TrainingFrequencyId))
            {
                this.ModelState.AddModelError(nameof(data.TrainingFrequencyId), "Invalid Training Frequency.");
            }

            if (!this.ModelState.IsValid)
            {
                data.Sports = this.accountService.GetSportsInAccountFormModel();
                data.Goals = this.accountService.GetGoalsInAccountFormModel();
                data.TrainingFrequencies = this.accountService.GetTrainingFrequenciesInAccountFormModel();

                return this.View(data);
            }

            this.accountService.CompleteAccount(userId, data.SportId, data.GoalId, data.GoalId, data.Weight, data.Height, data.Description);

            return this.RedirectToAction("Index", "Home");
        }

        private bool IsAccountComplete()
            => this.accountService.IsUserAccountComplete(this.User.GetId());
    }
}
