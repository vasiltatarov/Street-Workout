namespace StreetWorkout.Controllers
{
    using System.Linq;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using Data;
    using Models.Accounts;
    using Infrastructure;
    using StreetWorkout.Data.Models;

    [Authorize]
    public class AccountsController : Controller
    {
        private readonly StreetWorkoutDbContext data;

        public AccountsController(StreetWorkoutDbContext data)
        {
            this.data = data;
        }

        public IActionResult CompleteAccount()
        {
            if (this.data.Users.Find(this.User.GetId()).IsAccountCompleted)
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

            var userData = new UserData
            {
                UserId = user.Id,
                SportId = data.SportId,
                GoalId = data.GoalId,
                TrainingFrequencyId = data.TrainingFrequencyId,
                Weight = data.Weight,
                Height = data.Height,
                Description = data.Description,
            };

            this.data.UserDatas.AddRange(userData);
            user.IsAccountCompleted = true;

            this.data.SaveChanges();

            return this.RedirectToAction("Index", "Home");
        }
    }
}
