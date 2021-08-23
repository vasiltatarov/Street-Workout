namespace StreetWorkout.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static DataConstants.GroupWorkoutConstants;

    public class GroupWorkout
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(TitleMaxLength)]
        public string Title { get; set; }

        public int SportId { get; set; }

        public Sport Sport { get; set; }

        [Required]
        [MaxLength(AddressMaxLength)]
        public string Address { get; set; }

        public DateTime StartOn { get; set; }

        public DateTime EndOn { get; set; }

        public byte MaximumParticipants { get; set; }

        public byte PricePerPerson { get; set; }

        public string TrainerId { get; set; }

        public ApplicationUser Trainer { get; set; }

        [Required]
        public string Content { get; set; }

        public DateTime CreatedOn { get; set; }

        public bool IsDeleted { get; set; }

        public IEnumerable<UserWorkoutPayment> UserWorkoutPayments { get; set; } = new HashSet<UserWorkoutPayment>();
    }
}
