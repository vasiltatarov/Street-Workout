namespace StreetWorkout.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants;

    public class BodyPart
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(BodyPartNameMaxLength)]
        public string Name { get; set; }

        public IEnumerable<Workout> Workouts { get; set; } = new HashSet<Workout>();
    }
}
