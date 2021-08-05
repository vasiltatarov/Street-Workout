namespace StreetWorkout.Services.Supplements.Models
{
    using System.Collections.Generic;

    public class SupplementsQueryModel
    {
        public const int supplementsPerPage = 9;

        public int CurrentPage { get; set; } = 1;

        public int TotalSupplements { get; set; }

        public IEnumerable<SupplementServiceModel> Supplements { get; set; }
    }
}
