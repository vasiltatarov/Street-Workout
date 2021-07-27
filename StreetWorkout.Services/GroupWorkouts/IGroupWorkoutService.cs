namespace StreetWorkout.Services.GroupWorkouts
{
    using System;

    public interface IGroupWorkoutService
    {
        bool IsUserTrainer(string userId);

        void Create(string title, int sportId, string address, DateTime startOn, DateTime endOn, byte maximumParticipants, byte pricePerPerson, string trainerId, string content);

        GroupWorkoutsQueryModel All(int currentPage, string userId);
    }
}
