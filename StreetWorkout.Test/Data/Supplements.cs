namespace StreetWorkout.Test.Data
{
    using System.Linq;
    using System.Collections.Generic;
    using StreetWorkout.Data.Models;

    public static class Supplements
    {
        public static IEnumerable<Supplement> TenSupplements()
            => Enumerable.Range(1, 10)
                .Select(x => new Supplement
                {
                    Category = new SupplementCategory(),
                });
    }
}
