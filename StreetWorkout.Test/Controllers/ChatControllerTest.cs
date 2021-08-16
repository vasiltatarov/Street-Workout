using MyTested.AspNetCore.Mvc;
using StreetWorkout.Controllers;
using Xunit;

namespace StreetWorkout.Test.Controllers
{
    public class ChatControllerTest
    {
        [Fact]
        public void ChatShouldReturnView()
            => MyController<ChatController>
                .Instance()
                .WithUser()
                .Calling(c => c.Chat())
                .ShouldReturn()
                .View();
    }
}
