namespace StreetWorkout.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants;

    public class Sport
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(SportNameMaxLength)]
        public string Name { get; set; }

        public IEnumerable<Workout> Workouts { get; set; } = new HashSet<Workout>();

        public IEnumerable<GroupWorkout> GroupWorkouts { get; set; } = new HashSet<GroupWorkout>();
    }
}
