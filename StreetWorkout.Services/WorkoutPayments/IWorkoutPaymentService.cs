namespace StreetWorkout.Services.WorkoutPayments
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using StreetWorkout.Services.WorkoutPayments.Models;

    public interface IWorkoutPaymentService
    {
        Task<IEnumerable<UserWorkoutPaymentServiceModel>> All();
    }
}
