namespace StreetWorkout.Services.WorkoutPayments.Models
{
    using System;

    public class UserWorkoutPaymentServiceModel
    {
        public int Id { get; set; }

        public string User { get; set; }

        public string GroupWorkout { get; set; }

        public string FullName { get; set; }

        public string PhoneNumber { get; set; }

        public string Card { get; set; }

        public byte BoughtTickets { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
