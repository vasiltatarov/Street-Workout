namespace StreetWorkout.Test.Routing
{
    using MyTested.AspNetCore.Mvc;
    using StreetWorkout.Controllers;
    using Xunit;

    public class ChatControllerTest
    {
        [Fact]
        public void ChatShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Chat/Chat")
                .To<ChatController>(c => c.Chat());
    }
}
