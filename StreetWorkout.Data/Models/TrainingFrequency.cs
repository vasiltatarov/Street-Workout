namespace StreetWorkout.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using static DataConstants;

    public class TrainingFrequency
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(TrainingFrequencyNameMaxLength)]
        public string Name { get; set; }
    }
}
