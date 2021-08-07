namespace StreetWorkout.Services.Supplements.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using StreetWorkout.ViewModels.Supplements;

    public class SupplementsQueryModel
    {
        public const int SupplementsPerPage = 8;

        public int CurrentPage { get; set; } = 1;

        public int TotalSupplements { get; set; }

        [Display(Name = "Search by Text")]
        public string SearchTerms { get; set; }

        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        public IEnumerable<SupplementServiceModel> Supplements { get; set; }

        public IEnumerable<SupplementCategoryViewModel> Categories { get; set; }
    }
}
