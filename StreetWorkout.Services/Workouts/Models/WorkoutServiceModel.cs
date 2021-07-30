namespace StreetWorkout.Services.Workouts.Models
{
    public class WorkoutServiceModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Sport { get; set; }

        public string DifficultLevel { get; set; }

        public string BodyPart { get; set; }

        public string ImageUrl { get; set; }

        public int Minutes { get; set; }
    }
}
