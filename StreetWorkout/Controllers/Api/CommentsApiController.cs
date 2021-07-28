namespace StreetWorkout.Controllers.Api
{
    using Microsoft.AspNetCore.Mvc;

    using ViewModels.Comments;
    using Services.Comments;
    using Infrastructure;

    [Route("api/comments")]
    public class CommentsApiController : ApiController
    {
        private readonly ICommentService comments;

        public CommentsApiController(ICommentService comments)
            => this.comments = comments;

        [HttpPost]
        public CommentResponseModel Add(CommentInputModel input)
            => this.comments.Add(input.Content, this.User.GetId(), input.WorkoutId);
    }
}
