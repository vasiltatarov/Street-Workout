namespace StreetWorkout.ViewModels.Supplements
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static ViewModelConstants.SupplementFormModelConstants;

    public class SupplementFormModel
    {
        [Required]
        [StringLength(NameMaxLength, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = NameMinLength)]
        public string Name { get; set; }

        [Required]
        [Url]
        public string ImageUrl { get; set; }

        [Required]
        [MinLength(ContentMinLength)]
        public string Content { get; set; }

        [Display(Name = "Price in BGN")]
        [Range(PriceMinValue, PriceMaxValue)]
        public decimal Price { get; set; }

        [Display(Name = "Quantity in Grams")]
        [Range(QuantityMinValue, QuantityMaxValue)]
        public short Quantity { get; set; }

        public int CategoryId { get; set; }

        public IEnumerable<SupplementCategoryViewModel> Categories { get; set; }
    }
}
