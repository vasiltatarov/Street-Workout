namespace StreetWorkout.Services.Supplements
{
    using System.Linq;
    using System.Collections.Generic;
    using StreetWorkout.ViewModels.Supplements;
    using Data;
    using Data.Models;

    public class SupplementService : ISupplementService
    {
        private readonly StreetWorkoutDbContext data;

        public SupplementService(StreetWorkoutDbContext data)
            => this.data = data;

        public void Create(string name, int categoryId, string imageUrl, string content, decimal price, short quantity)
        {
            var supplement = new Supplement
            {
                Name = name,
                CategoryId = categoryId,
                ImageUrl = imageUrl,
                Content = content,
                Price = price,
                Quantity = quantity,
            };

            this.data.Supplements.Add(supplement);
            this.data.SaveChanges();
        }

        public IEnumerable<SupplementCategoryViewModel> GetSupplementCategories()
            => this.data.SupplementCategories
                .Select(x => new SupplementCategoryViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                })
                .ToList();

        public bool IsValidCategoryId(int id)
            => this.data
                .SupplementCategories
                .Any(x => x.Id == id);
    }
}
