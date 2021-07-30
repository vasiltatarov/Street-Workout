namespace StreetWorkout.Test.Mocks
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using Data;

    public static class DatabaseMock
    {
        public static StreetWorkoutDbContext Instance
        {
            get
            {
                var dbContextOptions = new DbContextOptionsBuilder<StreetWorkoutDbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString())
                    .Options;

                return new StreetWorkoutDbContext(dbContextOptions);
            }
        }
    }
}
