namespace StreetWorkout.Services.GroupWorkouts
{
    using System;

    public interface IGroupWorkoutService
    {
        void Create(string title, int sportId, string address, DateTime startOn, DateTime endOn, byte maximumParticipants, byte pricePerPerson, string trainerId, string content);
    }
}
