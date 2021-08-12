namespace StreetWorkout.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using static DataConstants.ChatMessageConstants;

    public class ChatMessage
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(TextMaxLength)]
        public string Text { get; set; }

        [Required]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
