namespace StreetWorkout.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using static DataConstants;

    public class UserData
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public int SportId { get; set; }

        public Sport Sport { get; set; }

        public int GoalId { get; set; }

        public Goal Goal { get; set; }

        public int TrainingFrequencyId { get; set; }

        public TrainingFrequency TrainingFrequency { get; set; }

        [MaxLength(WeightMaxValue)]
        public int Weight { get; set; }

        [MaxLength(HeightMaxValue)]
        public int Height { get; set; }

        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; }
    }
}
