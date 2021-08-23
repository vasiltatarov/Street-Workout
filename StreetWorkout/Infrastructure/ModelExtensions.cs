namespace StreetWorkout.Infrastructure
{
    using StreetWorkout.Services.Workouts.Models;

    public static class ModelExtensions
    {
        public static string GetInformation(this IWorkoutModel model)
            => $"{model.Title}";
    }
}
