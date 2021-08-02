namespace StreetWorkout.Services.GroupWorkouts
{
    using System;
    using Models;
    using StreetWorkout.ViewModels.GroupWorkouts;

    public interface IGroupWorkoutService
    {
        bool IsUserTrainer(string userId);

        bool IsUserCreator(string userId, int groupWorkoutId);

        void Create(string title, int sportId, string address, DateTime startOn, DateTime endOn, byte maximumParticipants, byte pricePerPerson, string trainerId, string content);

        bool Edit(int id, string title, int sportId, string address, DateTime startOn, DateTime endOn, byte maximumParticipants, byte pricePerPerson, string content);

        bool Delete(int id);

        byte AvailableTickets(int groupWorkoutId);

        void BuyTicket(string userId, int groupWorkoutId, string fullName, string phoneNumber, string card, byte boughtTickets);

        GroupWorkoutsQueryModel All(int currentPage, string userId);

        GroupWorkoutDetailsModel Details(int id);

        GroupWorkoutFormModel EditFormModel(int id);
    }
}
