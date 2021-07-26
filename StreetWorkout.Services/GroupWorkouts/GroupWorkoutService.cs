namespace StreetWorkout.Services.GroupWorkouts
{
    using System;
    using Data;
    using Data.Models;

    public class GroupWorkoutService : IGroupWorkoutService
    {
        private readonly StreetWorkoutDbContext data;

        public GroupWorkoutService(StreetWorkoutDbContext data)
            => this.data = data;

        public void Create(string title, int sportId, string address, DateTime startOn, DateTime endOn, byte maximumParticipants, byte pricePerPerson, string trainerId, string content)
        {
            var groupWorkout = new GroupWorkout
            {
                Title = title,
                SportId = sportId,
                Address = address,
                StartOn = startOn,
                EndOn = endOn,
                MaximumParticipants = maximumParticipants,
                PricePerPerson = pricePerPerson,
                TrainerId = trainerId,
                Content = content,
            };
            this.data.GroupWorkouts.Add(groupWorkout);
            this.data.SaveChanges();
        }
    }
}
