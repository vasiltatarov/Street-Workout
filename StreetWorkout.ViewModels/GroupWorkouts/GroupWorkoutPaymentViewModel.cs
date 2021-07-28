namespace StreetWorkout.ViewModels.GroupWorkouts
{
    using System.ComponentModel.DataAnnotations;

    public class GroupWorkoutPaymentViewModel
    {
        [Required]
        public string FullName { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        [CreditCard]
        public string Card { get; set; }

        [Range(1, 100)]
        public byte BoughtTickets { get; set; }
    }
}
