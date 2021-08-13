namespace StreetWorkout.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using Services.Chat;

    [Authorize]
    public class ChatController : Controller
    {
        private readonly IChatService chat;

        public ChatController(IChatService chat)
            => this.chat = chat;

        public async Task<IActionResult> Chat()
            => this.View(await this.chat.GetMessages());
    }
}
