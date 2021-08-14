namespace StreetWorkout.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants.PaymentConstants;

    public class Payment
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        [Required]
        [MaxLength(FirstNameMaxLength)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(LastNameMaxLength)]
        public string LastName { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MaxLength(AddressMaxLength)]
        public string Address { get; set; }

        [Required]
        [MaxLength(CardNameMaxLength)]
        public string CardName { get; set; }

        [Required]
        [MaxLength(CardNumberMaxLength)]
        public string CardNumber { get; set; }

        [Required]
        [MaxLength(ExpirationMaxLength)]
        public string Expiration { get; set; }

        public IEnumerable<SupplementPayment> SupplementPayments { get; set; } = new HashSet<SupplementPayment>();
    }
}
