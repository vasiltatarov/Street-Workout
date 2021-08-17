namespace StreetWorkout.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using Services.Accounts;
    using Infrastructure;
    using ViewModels.Accounts;

    using static WebConstants;

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

        public async Task<IActionResult> Edit(string userId)
        {
            if (!await this.accountService.IsUserAccountComplete(userId))
            {
                return BadRequest("Cannot edit this account because is not completed");
            }

            if (this.User.GetId() != userId && !this.User.IsInRole(AdministratorRoleName))
            {
                return Unauthorized();
            }

            var model = await this.accountService.GetEditFormModel(userId);
            model.Sports = await this.accountService.GetSportsInAccountFormModel();
            model.Goals = await this.accountService.GetGoalsInAccountFormModel();
            model.TrainingFrequencies = await this.accountService.GetTrainingFrequenciesInAccountFormModel();

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditFormModel model)
        {
            if (!await this.accountService.IsUserAccountComplete(model.Id))
            {
                return BadRequest("Cannot edit this account because is not completed");
            }

            if (this.User.GetId() != model.Id && !this.User.IsInRole(AdministratorRoleName))
            {
                return Unauthorized();
            }

            if (!await this.accountService.IsValidSportId(model.SportId))
            {
                this.ModelState.AddModelError(nameof(model.SportId), "Sport is Invalid.");
            }

            if (!await this.accountService.IsValidGoalId(model.GoalId))
            {
                this.ModelState.AddModelError(nameof(model.GoalId), "Goal is Invalid.");
            }

            if (!await this.accountService.IsValidTrainingFrequencyId(model.TrainingFrequencyId))
            {
                this.ModelState.AddModelError(nameof(model.TrainingFrequencyId), "Training Frequency is Invalid.");
            }

            if (!this.ModelState.IsValid)
            {
                model.Sports = await this.accountService.GetSportsInAccountFormModel();
                model.Goals = await this.accountService.GetGoalsInAccountFormModel();
                model.TrainingFrequencies = await this.accountService.GetTrainingFrequenciesInAccountFormModel();
                return View(model);
            }

            if (!await this.accountService.Edit(model.Id, model.ImageUrl, model.Weight, model.Height, model.City, model.Description, model.SportId, model.GoalId, model.TrainingFrequencyId))
            {
                return BadRequest();
            }

            return this.RedirectToAction("Account", new { username = model.Username });
        }

        public async Task<IActionResult> EditImage(string userId)
        {
            if (!this.User.IsInRole(AdministratorRoleName))
            {
                return Unauthorized();
            }

            return this.View(await this.accountService.GetEditImageFormModel(userId));
        }

        [HttpPost]
        public async Task<IActionResult> EditImage(EditImageFormModel model)
        {
            if (!this.User.IsInRole(AdministratorRoleName))
            {
                return Unauthorized();
            }

            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            if (!await this.accountService.EditImage(model.Id, model.ImageUrl))
            {
                return BadRequest();
            }

            return this.RedirectToAction("Account", new { username = model.UserName });
        }

        private async Task<bool> IsAccountComplete()
            => await this.accountService.IsUserAccountComplete(this.User.GetId());
    }
}
