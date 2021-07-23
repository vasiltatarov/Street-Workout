namespace StreetWorkout.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using Infrastructure;
    using Services.Comments;

    [Authorize]
    public class CommentsController : Controller
    {
        private readonly ICommentService comments;

        public CommentsController(ICommentService comments)
            => this.comments = comments;

        [HttpPost]
        public IActionResult Create(int workoutId, string content)
        {
            if (string.IsNullOrWhiteSpace(content) || content.Length < 10 || content.Length > 1000)
            {
                return this.BadRequest();
            }

            this.comments.Create(content, this.User.GetId(), workoutId);

            return this.RedirectToAction("Details", "Workouts", new { Id = workoutId });
        }
    }
}
