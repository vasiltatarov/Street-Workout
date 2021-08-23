namespace StreetWorkout.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using StreetWorkout.Infrastructure;
    using StreetWorkout.Services.Accounts;
    using StreetWorkout.ViewModels.Accounts;

    using static WebConstants;
    using static WebConstants.ModelStateMessage;

    [Authorize]
    public class AccountsController : Controller
    {
        private const string RedirectIndexActionName = nameof(HomeController.Index);
        private const string RedirectAccountActionName = nameof(Account);
        private const string RedirectControllerName = "Home";

        private readonly IAccountService accountService;

        public AccountsController(IAccountService accountService)
            => this.accountService = accountService;

        public async Task<IActionResult> Account(string username)
            => this.View(await this.accountService.GetAccount(username));

        public async Task<IActionResult> CompleteAccount()
            => await this.IsAccountComplete()
                ? this.RedirectToAction(RedirectIndexActionName, RedirectControllerName)
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
                return this.RedirectToAction(RedirectIndexActionName, RedirectControllerName);
            }

            if (!await this.accountService.IsValidSportId(data.SportId))
            {
                this.ModelState.AddModelError(nameof(data.SportId), InvalidSport);
            }

            if (!await this.accountService.IsValidGoalId(data.GoalId))
            {
                this.ModelState.AddModelError(nameof(data.GoalId), InvalidGoal);
            }

            if (!await this.accountService.IsValidTrainingFrequencyId(data.TrainingFrequencyId))
            {
                this.ModelState.AddModelError(nameof(data.TrainingFrequencyId), InvalidTrainingFrequency);
            }

            if (!this.ModelState.IsValid)
            {
                data.Sports = await this.accountService.GetSportsInAccountFormModel();
                data.Goals = await this.accountService.GetGoalsInAccountFormModel();
                data.TrainingFrequencies = await this.accountService.GetTrainingFrequenciesInAccountFormModel();

                return this.View(data);
            }

            await this.accountService.CompleteAccount(userId, data.SportId, data.GoalId, data.TrainingFrequencyId, data.Weight, data.Height, data.Description);

            return this.RedirectToAction(RedirectIndexActionName, RedirectControllerName);
        }

        public async Task<IActionResult> Edit(string userId)
        {
            if (!await this.accountService.IsUserAccountComplete(userId))
            {
                return this.BadRequest(CannotEditAccount);
            }

            if (this.User.GetId() != userId && !this.User.IsInRole(AdministratorRoleName))
            {
                return this.Unauthorized();
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
                return this.BadRequest(CannotEditAccount);
            }

            if (this.User.GetId() != model.Id && !this.User.IsInRole(AdministratorRoleName))
            {
                return this.Unauthorized();
            }

            if (!await this.accountService.IsValidSportId(model.SportId))
            {
                this.ModelState.AddModelError(nameof(model.SportId), InvalidSport);
            }

            if (!await this.accountService.IsValidGoalId(model.GoalId))
            {
                this.ModelState.AddModelError(nameof(model.GoalId), InvalidGoal);
            }

            if (!await this.accountService.IsValidTrainingFrequencyId(model.TrainingFrequencyId))
            {
                this.ModelState.AddModelError(nameof(model.TrainingFrequencyId), InvalidTrainingFrequency);
            }

            if (!this.ModelState.IsValid)
            {
                model.Sports = await this.accountService.GetSportsInAccountFormModel();
                model.Goals = await this.accountService.GetGoalsInAccountFormModel();
                model.TrainingFrequencies = await this.accountService.GetTrainingFrequenciesInAccountFormModel();
                return this.View(model);
            }

            if (!await this.accountService.Edit(model.Id, model.ImageUrl, model.Weight, model.Height, model.City, model.Description, model.SportId, model.GoalId, model.TrainingFrequencyId))
            {
                return this.BadRequest();
            }

            return this.RedirectToAction(RedirectAccountActionName, new { username = model.Username });
        }

        public async Task<IActionResult> EditImage(string userId)
        {
            if (!this.User.IsInRole(AdministratorRoleName))
            {
                return this.Unauthorized();
            }

            return this.View(await this.accountService.GetEditImageFormModel(userId));
        }

        [HttpPost]
        public async Task<IActionResult> EditImage(EditImageFormModel model)
        {
            if (!this.User.IsInRole(AdministratorRoleName))
            {
                return this.Unauthorized();
            }

            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            if (!await this.accountService.EditImage(model.Id, model.ImageUrl))
            {
                return this.BadRequest();
            }

            return this.RedirectToAction(RedirectAccountActionName, new { username = model.UserName });
        }

        private async Task<bool> IsAccountComplete()
            => await this.accountService.IsUserAccountComplete(this.User.GetId());
    }
}
