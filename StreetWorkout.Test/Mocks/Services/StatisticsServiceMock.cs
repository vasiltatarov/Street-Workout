namespace StreetWorkout.Test.Mocks.Services
{
    using Moq;
    using StreetWorkout.Services.Statistics;
    using StreetWorkout.Services.Statistics.Models;

    public static class StatisticsServiceMock
    {
        public static IStatisticsService Instance
        {
            get
            {
                var statisticsServiceMock = new Mock<IStatisticsService>();

                statisticsServiceMock.Setup(x => x.Total())
                    .Returns(new StatisticsModel
                    {
                        TotalEnthusiasts = 10,
                        TotalTrainers = 5,
                        TotalWorkouts = 20,
                    });

                return statisticsServiceMock.Object;
            }
        }
    }
}
