namespace StreetWorkout.Controllers.Api
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using ViewModels.Comments;
    using Services.Comments;
    using Infrastructure;

    [ApiController]
    [Route("api/comments")]
    [Authorize]
    [IgnoreAntiforgeryToken]
    public class CommentsApiController : ControllerBase
    {
        private readonly ICommentService comments;

        public CommentsApiController(ICommentService comments)
            => this.comments = comments;

        [HttpPost]
        public CommentResponseModel Add(CommentInputModel input)
            => this.comments.Add(input.Content, this.User.GetId(), input.WorkoutId);
    }
}
