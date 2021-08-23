namespace StreetWorkout.Test.Controllers
{
    using MyTested.AspNetCore.Mvc;
    using StreetWorkout.Controllers;
    using Xunit;

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
