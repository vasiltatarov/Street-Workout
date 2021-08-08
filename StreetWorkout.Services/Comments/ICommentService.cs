namespace StreetWorkout.Services.Comments
{
    using StreetWorkout.ViewModels.Comments;

    public interface ICommentService
    {
        CommentResponseModel Add(string content, string userId, int workoutId);

        bool IsValidWorkoutId(int id);
    }
}
