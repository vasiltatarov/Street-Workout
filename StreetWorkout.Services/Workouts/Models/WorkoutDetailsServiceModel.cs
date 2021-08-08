namespace StreetWorkout.Services.Workouts.Models
{
    using System;
    using System.Collections.Generic;

    public class WorkoutDetailsServiceModel : IWorkoutModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Sport { get; set; }

        public string DifficultLevel { get; set; }

        public string BodyPart { get; set; }

        public string ImageUrl { get; set; }

        public string UserUsername { get; set; }

        public string UserImageUrl { get; set; }

        public string UserDescription { get; set; }

        public int Minutes { get; set; }

        public string Content { get; set; }

        public DateTime CreatedOn { get; set; }

        public IEnumerable<string> Sports { get; set; }

        public IEnumerable<WorkoutDetailsLatestTraining> LatestWorkouts { get; set; }

        public IEnumerable<CommentInDetailsServiceModel> Comments { get; set; }
    }
}
