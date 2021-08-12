namespace StreetWorkout.Services.Chat
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using StreetWorkout.ViewModels.Chat;

    public interface IChatService
    {
        Task Create(string text, string userId);

        IEnumerable<Message> GetMessages();
    }
}
