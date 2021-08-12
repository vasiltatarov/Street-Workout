namespace StreetWorkout.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using Services.Accounts;
    using Infrastructure;
    using ViewModels.Accounts;

    [Authorize]
    public class AccountsController : Controller
    {
        private readonly IAccountService accountService;

        public AccountsController(IAccountService accountService)
            => this.accountService = accountService;

        public async Task<IActionResult> Account(string username)
            => this.View(await this.accountService.GetAccount(username));

        public async Task<IActionResult> CompleteAccount()
            => await this.IsAccountComplete()
                ? this.RedirectToAction("Index", "Home")
                : this.View(new AccountFormModel
                {
                    Sports = await this.accountService.GetSportsInAccountFormModel(),
                    Goals = await this.accountService.GetGoalsInAccountFormModel(),
                    TrainingFrequencies = await this.accountService.GetTrainingFrequenciesInAccountFormModel(),
                });

        [HttpPost]
        public async Task<IActionResult> CompleteAccount(AccountFormModel data)
        {
            var userId = this.User.GetId();

            if (await this.IsAccountComplete() || await this.accountService.IsUserDataExists(userId))
            {
                return this.RedirectToAction("Index", "Home");
            }

            if (!await this.accountService.IsValidSportId(data.SportId))
            {
                this.ModelState.AddModelError(nameof(data.SportId), "Invalid Sport.");
            }

            if (!await this.accountService.IsValidGoalId(data.GoalId))
            {
                this.ModelState.AddModelError(nameof(data.GoalId), "Invalid Goal.");
            }

            if (!await this.accountService.IsValidTrainingFrequencyId(data.TrainingFrequencyId))
            {
                this.ModelState.AddModelError(nameof(data.TrainingFrequencyId), "Invalid Training Frequency.");
            }

            if (!this.ModelState.IsValid)
            {
                data.Sports = await this.accountService.GetSportsInAccountFormModel();
                data.Goals = await this.accountService.GetGoalsInAccountFormModel();
                data.TrainingFrequencies = await this.accountService.GetTrainingFrequenciesInAccountFormModel();

                return this.View(data);
            }

            await this.accountService.CompleteAccount(userId, data.SportId, data.GoalId, data.TrainingFrequencyId, data.Weight, data.Height, data.Description);

            return this.RedirectToAction("Index", "Home");
        }

        private async Task<bool> IsAccountComplete()
            => await this.accountService.IsUserAccountComplete(this.User.GetId());
    }
}
