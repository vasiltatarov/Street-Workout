namespace StreetWorkout.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Identity;

    public class ApplicationUser : IdentityUser
    {
        [Url]
        public string ImageUrl { get; set; }

        public int CountryId { get; set; }

        public Country Country { get; set; }
    }
}
