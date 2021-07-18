namespace StreetWorkout.ViewModels.Accounts
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static Common.DataConstants;

    public class AccountFormModel
    {
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

        public IEnumerable<SportInAccountViewModel> Sports { get; set; }

        public IEnumerable<GoalInAccountViewModel> Goals { get; set; }

        public IEnumerable<TrainingFrequencyInAccountViewModel> TrainingFrequencies { get; set; }
    }
}
