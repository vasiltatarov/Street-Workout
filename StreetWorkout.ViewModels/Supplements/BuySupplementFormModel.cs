namespace StreetWorkout.ViewModels.Supplements
{
    using System.ComponentModel.DataAnnotations;

    using static ViewModelConstants.BuySupplementFormModelConstants;

    public class BuySupplementFormModel
    {
        public BuySupplementViewModel SupplementModel { get; set; }

        public byte DeliveryPrice { get; set; } = 4;

        public byte VAT { get; set; } = 1;

        public int WorkoutId { get; set; }

        [Display(Name = "First Name")]
        [Required]
        [StringLength(FirstNameMaxLength, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = FirstNameMinLength)]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required]
        [StringLength(LastNameMaxLength, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = LastNameMinLength)]
        public string LastName { get; set; }

        [Display(Name = "Phone Number")]
        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(AddressMaxLength, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = AddressMinLength)]
        public string Address { get; set; }

        [Display(Name = "Card Name")]
        [Required]
        public string CardName { get; set; }

        [Display(Name = "Card Number")]
        [Required]
        public string CardNumber { get; set; }

        [Display(Name = "Expiration")]
        [Required]
        public string Expiration { get; set; }
    }
}
