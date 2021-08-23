namespace StreetWorkout.Controllers.Api
{
    using Microsoft.AspNetCore.Mvc;
    using StreetWorkout.Infrastructure;
    using StreetWorkout.Services.Comments;
    using StreetWorkout.ViewModels.Comments;

    [Route("api/comments")]
    public class CommentsApiController : ApiController
    {
        private readonly ICommentService comments;

        public CommentsApiController(ICommentService comments)
            => this.comments = comments;

        [HttpPost]
        public ActionResult<CommentResponseModel> Add(CommentInputModel input)
        {
            if (!this.comments.IsValidWorkoutId(input.WorkoutId))
            {
                return this.BadRequest();
            }

            return this.comments.Add(input.Content, this.User.GetId(), input.WorkoutId);
        }
    }
}
