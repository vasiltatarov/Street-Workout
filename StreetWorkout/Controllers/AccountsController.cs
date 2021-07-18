namespace StreetWorkout.Controllers
{
    using System.Linq;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using Data;
    using Services;
    using StreetWorkout.Data.Models;
    using Models.Accounts;
    using Infrastructure;

    [Authorize]
    public class AccountsController : Controller
    {
        private readonly StreetWorkoutDbContext data;
        private readonly IAccountService accountService;

        public AccountsController(StreetWorkoutDbContext data, IAccountService accountService)
        {
            this.data = data;
            this.accountService = accountService;
        }

        public IActionResult CompleteAccount()
        {
            if (this.IsAccountComplete())
            {
                return this.RedirectToAction("Index", "Home");
            }

            return this.View(new AccountFormModel
            {
                Sports = this.data.Sports
                    .Select(x => new SportInAccountViewModel
                    {
                        Id = x.Id,
                        Name = x.Name,
                    })
                    .ToList(),
                Goals = this.data.Goals
                    .Select(x => new GoalInAccountViewModel
                    {
                        Id = x.Id,
                        Name = x.Name,
                    })
                    .ToList(),
                TrainingFrequencies = this.data.TrainingFrequencies
                    .Select(x => new TrainingFrequencyInAccountViewModel
                    {
                        Id = x.Id,
                        Name = x.Name,
                    })
                    .ToList(),
            });
        }

        [HttpPost]
        public IActionResult CompleteAccount(AccountFormModel data)
        {
            var user = this.data.Users.Find(this.User.GetId());

            if (user.IsAccountCompleted || this.data.UserDatas.Any(x => x.UserId == user.Id))
            {
                return this.RedirectToAction("Index", "Home");
            }

            if (!this.data.Sports.Any(x => x.Id == data.SportId))
            {
                this.ModelState.AddModelError(nameof(data.SportId), "Invalid Sport.");
            }

            if (!this.data.Goals.Any(x => x.Id == data.GoalId))
            {
                this.ModelState.AddModelError(nameof(data.GoalId), "Invalid Goal.");
            }

            if (!this.data.TrainingFrequencies.Any(x => x.Id == data.TrainingFrequencyId))
            {
                this.ModelState.AddModelError(nameof(data.TrainingFrequencyId), "Invalid Training Frequency.");
            }

            if (!this.ModelState.IsValid)
            {
                data.Sports = this.data
                    .Sports
                    .Select(x => new SportInAccountViewModel
                    {
                        Id = x.Id,
                        Name = x.Name,
                    })
                    .ToList();

                data.Goals = this.data
                    .Goals
                    .Select(x => new GoalInAccountViewModel
                    {
                        Id = x.Id,
                        Name = x.Name,
                    })
                    .ToList();

                data.TrainingFrequencies = this.data
                    .TrainingFrequencies
                    .Select(x => new TrainingFrequencyInAccountViewModel
                    {
                        Id = x.Id,
                        Name = x.Name,
                    }).ToList();

                return this.View(data);
            }

            this.accountService.CompleteAccount(user.Id, data.SportId, data.GoalId, data.GoalId, data.Weight, data.Height, data.Description);

            return this.RedirectToAction("Index", "Home");
        }

        private bool IsAccountComplete()
            => this.accountService.IsUserAccountComplete(this.User.GetId());
    }
}
