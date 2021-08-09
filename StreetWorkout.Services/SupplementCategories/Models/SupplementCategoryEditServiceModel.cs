namespace StreetWorkout.Services.SupplementCategories.Models
{
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstants.SupplementCategoryConstants;

    public class SupplementCategoryEditServiceModel
    {
        [Required]
        [MaxLength(SupplementCategoryNameMaxLength)]
        public string Name { get; set; }
    }
}
