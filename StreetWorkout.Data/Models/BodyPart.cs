namespace StreetWorkout.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using static Common.DataConstants;

    public class BodyPart
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(BodyPartNameMaxLength)]
        public string Name { get; set; }
    }
}
