namespace StreetWorkout.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using static DataConstants.Country;

    public class Country
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; init; }
    }
}
