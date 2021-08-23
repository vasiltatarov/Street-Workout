using StreetWorkout.Data.Models.Enums;

namespace StreetWorkout.Areas.Identity.Pages.Account
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.AspNetCore.WebUtilities;

    using Data.Models;
    using Data;

    using static Data.DataConstants;

    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly StreetWorkoutDbContext data;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            StreetWorkoutDbContext data)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.data = data;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public IEnumerable<Country> Countries { get; set; }

        public class InputModel
        {
            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Username")]
            [StringLength(UsernameMaxLength, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = UsernameMinLength)]
            public string UserName { get; set; }

            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            [Display(Name = "Image URL")]
            [Url]
            public string ImageUrl { get; set; }

            [Display(Name = "Country")]
            [Range(1, 241, ErrorMessage = "Please select valid Country.")]
            public int CountryId { get; set; }

            [Required]
            [StringLength(CityMaxLength, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = CityMinLength)]
            public string City { get; set; }

            [Display(Name = "Select your role")]
            [EnumDataType(typeof(UserRole), ErrorMessage = "Please select valid user role.")]
            [Range(1, 2, ErrorMessage = "Please select valid user role.")]
            public UserRole UserRole { get; set; }

            [EnumDataType(typeof(Gender), ErrorMessage = "Please select valid gender.")]
            [Range(1, 2, ErrorMessage = "Please select valid gender.")]
            public Gender Gender { get; set; }

            [Display(Name = "Date Of Birth")]
            [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
            public DateTime DateOfBirth { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            Countries = this.data
                .Countries
                .OrderBy(x => x.Name);
            ReturnUrl = returnUrl;
            ExternalLogins = (await this.signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await this.signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            if (!await this.data.Countries.AnyAsync(x => x.Id == Input.CountryId))
            {
                ModelState.AddModelError(nameof(Input.CountryId), "Please select valid Country.");
            }

            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = Input.UserName,
                    Email = Input.Email,
                    ImageUrl = Input.ImageUrl ?? "https://pbs.twimg.com/profile_images/746460305396371456/4QYRblQD_400x400.jpg",
                    CountryId = Input.CountryId,
                    City = Input.City,
                    Gender = Input.Gender,
                    UserRole = Input.UserRole,
                    DateOfBirth = Input.DateOfBirth,
                    IsAccountCompleted = false,
                    CreatedOn = DateTime.UtcNow,
                };
                var result = await this.userManager.CreateAsync(user, Input.Password);

                if (result.Succeeded)
                {
                    var code = await this.userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    if (this.userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await this.signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            Countries = this.data.Countries;
            return Page();
        }
    }
}
