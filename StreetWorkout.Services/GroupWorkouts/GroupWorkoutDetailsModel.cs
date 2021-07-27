namespace StreetWorkout.Services.GroupWorkouts
{
    using System;

    public class GroupWorkoutDetailsModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string ImageUrl { get; set; }

        public string Sport { get; set; }

        public string Address { get; set; }

        public byte MaximumParticipants { get; set; }

        public byte PricePerPerson { get; set; }

        public string Content { get; set; }

        public string TrainerUsername { get; set; }

        public string TrainerImageUrl { get; set; }

        public string TrainerDescription { get; set; }

        public DateTime StartOn { get; set; }

        public DateTime EndOn { get; set; }

        public string CreatedOn { get; set; }
    }
}
