namespace StreetWorkout.Services.GroupWorkouts
{
    using System;
    using System.Threading.Tasks;

    using StreetWorkout.Services.GroupWorkouts.Models;
    using StreetWorkout.ViewModels.GroupWorkouts;

    public interface IGroupWorkoutService
    {
        Task<bool> IsUserTrainer(string userId);

        Task<bool> IsUserCreator(string userId, int groupWorkoutId);

        Task Create(string title, int sportId, string address, DateTime startOn, DateTime endOn, byte maximumParticipants, byte pricePerPerson, string trainerId, string content);

        Task<bool> Edit(int id, string title, int sportId, string address, DateTime startOn, DateTime endOn, byte maximumParticipants, byte pricePerPerson, string content);

        Task<bool> Delete(int id);

        Task<byte> AvailableTickets(int groupWorkoutId);

        Task BuyTicket(string userId, int groupWorkoutId, string fullName, string phoneNumber, string card, byte boughtTickets);

        Task<GroupWorkoutsQueryModel> All(int currentPage, string userId);

        Task<GroupWorkoutDetailsModel> Details(int id);

        Task<GroupWorkoutFormModel> EditFormModel(int id);
    }
}
