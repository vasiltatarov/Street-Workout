namespace StreetWorkout.Hubs
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.SignalR;
    using StreetWorkout.Data.Models;
    using StreetWorkout.Services.Chat;
    using StreetWorkout.ViewModels.Chat;

    [Authorize]
    public class ChatHub : Hub
    {
        private const int MessageMinLength = 2;
        private const int MessageMaxLength = 300;

        private readonly UserManager<ApplicationUser> userManager;
        private readonly IChatService chat;

        public ChatHub(UserManager<ApplicationUser> userManager, IChatService chat)
        {
            this.userManager = userManager;
            this.chat = chat;
        }

        public async Task Send(string message)
        {
            if (message.Length < MessageMinLength || message.Length > MessageMaxLength)
            {
                return;
            }

            var user = await this.userManager.GetUserAsync(this.Context.User);
            await this.chat.Create(message, user.Id);

            await this.Clients.All.SendAsync(
                "NewMessage",
                new Message
                {
                    Text = message,
                    Username = user.UserName,
                    UserImageUrl = user.ImageUrl,
                    CreatedOn = DateTime.UtcNow,
                });
        }
    }
}
