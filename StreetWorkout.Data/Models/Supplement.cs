namespace StreetWorkout.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using static DataConstants.SupplementConstants;

    public class Supplement
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        public int CategoryId { get; set; }

        public SupplementCategory Category { get; set; }

        [Required]
        [Url]
        public string ImageUrl { get; set; }

        [Required]
        public string Content { get; set; }

        public decimal Price { get; set; }

        public short Quantity { get; set; }

        public bool IsDeleted { get; set; }
    }
}
