namespace StreetWorkout.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants.SupplementCategoryConstants;

    public class SupplementCategory
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(SupplementCategoryMaxLength)]
        public string Name { get; set; }

        public IEnumerable<Supplement> Supplements { get; set; }
    }
}
