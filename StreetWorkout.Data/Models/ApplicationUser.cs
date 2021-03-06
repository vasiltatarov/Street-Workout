namespace StreetWorkout.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Identity;

    using StreetWorkout.Data.Models.Enums;

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

        public DateTime CreatedOn { get; set; }

        public IEnumerable<Workout> Workouts { get; set; } = new HashSet<Workout>();

        public IEnumerable<Vote> Votes { get; set; } = new HashSet<Vote>();

        public IEnumerable<GroupWorkout> GroupWorkouts { get; set; } = new HashSet<GroupWorkout>();

        public IEnumerable<UserWorkoutPayment> UserWorkoutPayments { get; set; } = new HashSet<UserWorkoutPayment>();

        public IEnumerable<ChatMessage> ChatMessages { get; set; } = new HashSet<ChatMessage>();
    }
}
