namespace StreetWorkout.Hubs
{
    using System;
    using System.Threading.Tasks;
    
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.SignalR;

    using ViewModels.Chat;
    using Data.Models;

    [Authorize]
    public class ChatHub : Hub
    {
        private readonly UserManager<ApplicationUser> userManager;

        public ChatHub(UserManager<ApplicationUser> userManager)
            => this.userManager = userManager;

        public async Task Send(string message)
        {
            var user = await this.userManager.GetUserAsync(this.Context.User);

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
