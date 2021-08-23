namespace StreetWorkout.Services.Statistics
{
    using System.Linq;
    using StreetWorkout.Data;
    using StreetWorkout.Data.Models.Enums;
    using StreetWorkout.Services.Statistics.Models;

    public class StatisticsService : IStatisticsService
    {
        private readonly StreetWorkoutDbContext data;

        public StatisticsService(StreetWorkoutDbContext data)
            => this.data = data;

        public StatisticsModel Total()
            => new ()
            {
                TotalTrainers = this.data.Users.Count(x => x.UserRole == UserRole.Trainer),
                TotalEnthusiasts = this.data.Users.Count(x => x.UserRole == UserRole.Enthusiast),
                TotalWorkouts = this.data.Workouts.Count(),
            };
    }
}
