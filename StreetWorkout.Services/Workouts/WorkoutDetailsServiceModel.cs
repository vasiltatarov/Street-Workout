namespace StreetWorkout.Services.Workouts
{
    public class WorkoutDetailsServiceModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Sport { get; set; }

        public string DifficultLevel { get; set; }

        public string BodyPart { get; set; }

        public string UserUsername { get; set; }

        public string UserImageUrl { get; set; }

        public int Minutes { get; set; }

        public string Content { get; set; }
    }
}
