namespace StreetWorkout.Services.Comments
{
    using System;
    using Data;
    using Data.Models;

    public class CommentService : ICommentService
    {
        private readonly StreetWorkoutDbContext data;

        public CommentService(StreetWorkoutDbContext data)
            => this.data = data;

        public void Create(string content, string userId, int workoutId)
        {
            var comment = new Comment
            {
                Content = content,
                UserId = userId,
                WorkoutId = workoutId,
                CreatedOn = DateTime.UtcNow,
            };
            this.data.Comments.Add(comment);
            this.data.SaveChanges();
        }
    }
}
