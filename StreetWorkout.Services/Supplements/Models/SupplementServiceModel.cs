namespace StreetWorkout.Services.Supplements.Models
{
    public class SupplementServiceModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Category { get; set; }

        public string ImageUrl { get; set; }

        public string Content { get; set; }

        public decimal Price { get; set; }

        public short Quantity { get; set; }
    }
}
