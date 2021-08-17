namespace StreetWorkout.ViewModels.Accounts
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Workouts;

    using static ViewModelConstants.AccountFormModelConstants;
    using static ViewModelConstants.EditFormModelConstants;

    public class EditFormModel
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        [Url]
        public string ImageUrl { get; set; }

        [Display(Name = "Sport")]
        public int SportId { get; set; }

        [Display(Name = "Goal")]
        public int GoalId { get; set; }

        [Display(Name = "Training Frequency")]
        public int TrainingFrequencyId { get; set; }

        [Display(Name = "Weight in Kilograms (Example: 56)")]
        [Range(WeightMinValue, WeightMaxValue)]
        public int Weight { get; set; }

        [Display(Name = "Height in Centimeters (Example: 172)")]
        [Range(HeightMinValue, HeightMaxValue)]
        public int Height { get; set; }

        [StringLength(DescriptionMaxLength, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = DescriptionMinLength)]
        public string Description { get; set; }

        [Required]
        [StringLength(CityMaxLength, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = CityMinLength)]
        public string City { get; set; }

        public IEnumerable<SportViewModel> Sports { get; set; }

        public IEnumerable<GoalInAccountViewModel> Goals { get; set; }

        public IEnumerable<TrainingFrequencyInAccountViewModel> TrainingFrequencies { get; set; }
    }
}
