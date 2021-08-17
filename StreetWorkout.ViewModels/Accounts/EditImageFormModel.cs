namespace StreetWorkout.ViewModels.Accounts
{
    using System.ComponentModel.DataAnnotations;

    public class EditImageFormModel
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        [Url]
        public string ImageUrl { get; set; }
    }
}
