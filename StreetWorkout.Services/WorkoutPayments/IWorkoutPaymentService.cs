namespace StreetWorkout.Services.WorkoutPayments
{
    using System.Collections.Generic;
    using Models;

    public interface IWorkoutPaymentService
    {
        IEnumerable<UserWorkoutPaymentServiceModel> All();
    }
}
