namespace StreetWorkout.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using static DataConstants;

    public class Goal
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(GoalNameMaxLength)]
        public string Name { get; set; }
    }
}
