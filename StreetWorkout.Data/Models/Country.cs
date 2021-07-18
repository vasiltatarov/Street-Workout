namespace StreetWorkout.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.Collections.Generic;

    using static Common.DataConstants;

    public class Country
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(CountryNameMaxLength)]
        public string Name { get; init; }

        public IEnumerable<ApplicationUser> Users { get; set; }
    }
}
