namespace StreetWorkout.Infrastructure
{
    using Services.Workouts.Models;

    public static class ModelExtensions
    {
        public static string GetInformation(this IWorkoutModel model)
            => $"{model.Title}_{model.Sport}";
    }
}
