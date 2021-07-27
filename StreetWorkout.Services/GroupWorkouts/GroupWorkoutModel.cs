namespace StreetWorkout.Services.GroupWorkouts
{
    public class GroupWorkoutModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string ImageUrl { get; set; }

        public string Sport { get; set; }

        public string Address { get; set; }

        public byte MaximumParticipants { get; set; }

        public byte PricePerPerson { get; set; }

        public string StartOn { get; set; }
    }
}
