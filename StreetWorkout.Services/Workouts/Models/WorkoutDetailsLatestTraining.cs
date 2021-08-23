namespace StreetWorkout.Services.Workouts.Models
{
    using System;

    public class WorkoutDetailsLatestTraining : IWorkoutModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Sport { get; set; }

        public string ImageUrl { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
