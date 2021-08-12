namespace StreetWorkout.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Services.Chat;

    [Authorize]
    public class ChatController : Controller
    {
        private readonly IChatService chat;

        public ChatController(IChatService chat)
            => this.chat = chat;

        public IActionResult Chat()
            => this.View(this.chat.GetMessages());
    }
}
