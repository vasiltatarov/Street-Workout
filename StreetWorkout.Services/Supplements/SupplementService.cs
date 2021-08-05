namespace StreetWorkout.Services.Supplements
{
    using System.Linq;
    using System.Collections.Generic;

    using StreetWorkout.ViewModels.Supplements;
    using StreetWorkout.Services.Supplements.Models;
    using Data;
    using Data.Models;

    public class SupplementService : ISupplementService
    {
        private readonly StreetWorkoutDbContext data;

        public SupplementService(StreetWorkoutDbContext data)
            => this.data = data;

        public SupplementsQueryModel All(int currentPage)
        {
            var supplementsQuery = this.data
                .Supplements
                .Where(x => !x.IsDeleted)
                .AsQueryable();

            var supplements = supplementsQuery
                .Skip((currentPage - 1) * SupplementsQueryModel.supplementsPerPage)
                .Take(SupplementsQueryModel.supplementsPerPage)
                .Select(x => new SupplementServiceModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Category = x.Category.Name,
                    ImageUrl = x.ImageUrl,
                    ShortContent = x.Content.Substring(0, 300),
                    Price = x.Price,
                    Quantity = x.Quantity,
                })
                .ToList();

            return new SupplementsQueryModel
            {
                 CurrentPage = currentPage,
                 Supplements = supplements,
                 TotalSupplements = supplementsQuery.Count(),
            };
        }

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
