﻿namespace StreetWorkout.Test.Data
{
    using System.Linq;
    using System.Collections.Generic;
    using StreetWorkout.Data.Models;

    public static class GroupWorkouts
    {
        public static IEnumerable<GroupWorkout> TenGroupWorkouts()
            => Enumerable.Range(1, 10)
                .Select(x => new GroupWorkout
                {
                    Sport = new Sport(),
                    Trainer = new ApplicationUser(),
                });
    }
}
