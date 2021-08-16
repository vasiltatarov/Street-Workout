namespace StreetWorkout.Test.Routing
{
    using MyTested.AspNetCore.Mvc;
    using Xunit;
    using StreetWorkout.Controllers;

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
