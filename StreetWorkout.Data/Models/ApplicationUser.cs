namespace StreetWorkout.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Identity;

    using static DataConstants;

    public class ApplicationUser : IdentityUser
    {
        [Url]
        public string ImageUrl { get; set; }

        public int CountryId { get; set; }

        public Country Country { get; set; }

        [MaxLength(CityMaxLength)]
        public string City { get; set; }

        public UserRole UserRole { get; set; }

        public Gender Gender { get; set; }

        public DateTime DateOfBirth { get; set; }

        public bool IsAccountCompleted { get; set; }
    }
}
