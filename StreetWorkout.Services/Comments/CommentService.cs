using System.Linq;

namespace StreetWorkout.Services.Comments
{
    using System;
    using Data;
    using Data.Models;
    using StreetWorkout.ViewModels.Comments;

    public class CommentService : ICommentService
    {
        private readonly StreetWorkoutDbContext data;

        public CommentService(StreetWorkoutDbContext data)
            => this.data = data;

        public CommentResponseModel Add(string content, string userId, int workoutId)
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

            var user = this.data.Users.Find(userId);

            return new CommentResponseModel
            {
                UserImageUrl = user.ImageUrl,
                UserUsername = user.UserName,
                Content = content,
            };
        }

        public bool IsValidWorkoutId(int id)
            => this.data.Workouts
                .Any(x => x.Id == id);
    }
}
