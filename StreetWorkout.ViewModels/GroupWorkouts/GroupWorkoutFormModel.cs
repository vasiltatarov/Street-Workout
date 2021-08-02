namespace StreetWorkout.ViewModels.GroupWorkouts
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Workouts;

    using static Common.GroupWorkoutConstants;

    public class GroupWorkoutFormModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(TitleMaxLength, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = TitleMinLength)]
        public string Title { get; set; }

        [Display(Name = "Sport")]
        public int SportId { get; set; }

        [Required]
        [StringLength(AddressMaxLength, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = AddressMinLength)]
        public string Address { get; set; }

        [Display(Name = "Start Workout Time")]
        public DateTime StartOn { get; set; }

        [Display(Name = "End Workout Time")]
        public DateTime EndOn { get; set; }

        [Display(Name = "Maximum Participants")]
        [Range(MaximumParticipantsMinValue, MaximumParticipantsMaxValue)]
        public byte MaximumParticipants { get; set; }

        [Display(Name = "Price Per Person")]
        [Range(PricePerPersonMinValue, PricePerPersonMaxValue)]
        public byte PricePerPerson { get; set; }

        [Required]
        public string Content { get; set; }

        public IEnumerable<SportViewModel> Sports { get; set; }
    }
}
