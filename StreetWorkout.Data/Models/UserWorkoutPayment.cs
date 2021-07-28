namespace StreetWorkout.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants.UserWorkoutPaymentConstants;

    public class UserWorkoutPayment
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public int GroupWorkoutId { get; set; }

        public GroupWorkout GroupWorkout { get; set; }

        [Required]
        [MaxLength(FullNameMaxLength)]
        public string FullName { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        [CreditCard]
        public string Card { get; set; }

        public byte BoughtTickets { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
