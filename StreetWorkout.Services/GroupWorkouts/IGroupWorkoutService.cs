namespace StreetWorkout.Services.GroupWorkouts
{
    using System;
    using Models;

    public interface IGroupWorkoutService
    {
        bool IsUserTrainer(string userId);

        void Create(string title, int sportId, string address, DateTime startOn, DateTime endOn, byte maximumParticipants, byte pricePerPerson, string trainerId, string content);

        byte AvailableTickets(int groupWorkoutId);

        void BuyTicket(string userId, int groupWorkoutId, string fullName, string phoneNumber, string card, byte boughtTickets);

        GroupWorkoutsQueryModel All(int currentPage, string userId);

        GroupWorkoutDetailsModel Details(int id);
    }
}
