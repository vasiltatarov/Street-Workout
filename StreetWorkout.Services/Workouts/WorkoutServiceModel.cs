namespace StreetWorkout.Services.Workouts
{
    public class WorkoutServiceModel
    {
        public string Title { get; set; }

        public string Sport { get; set; }

        public string DifficultLevel { get; set; }

        public string BodyPart { get; set; }

        public string TrainerUsername { get; set; }

        public string TrainerImageUrl { get; set; }

        public int Minutes { get; set; }

        public string Content { get; set; }
    }
}
