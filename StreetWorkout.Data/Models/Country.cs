namespace StreetWorkout.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static DataConstants;

    public class Country
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(CountryNameMaxLength)]
        public string Name { get; init; }

        public IEnumerable<ApplicationUser> Users { get; set; }
    }
}
