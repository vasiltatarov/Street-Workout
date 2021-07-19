﻿namespace StreetWorkout.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using static Common.DataConstants;

    public class Sport
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(SportNameMaxLength)]
        public string Name { get; set; }
    }
}