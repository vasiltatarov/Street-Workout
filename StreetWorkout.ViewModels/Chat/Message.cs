namespace StreetWorkout.ViewModels.Chat
{
    using System;

    public class Message
    {
        public string Text { get; set; }

        public string Username { get; set; }

        public string UserImageUrl { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
