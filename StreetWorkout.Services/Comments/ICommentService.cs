namespace StreetWorkout.Services.Comments
{
    public interface ICommentService
    {
        void Create(string content, string userId, int workoutId);
    }
}
