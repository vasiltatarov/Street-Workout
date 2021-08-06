namespace StreetWorkout.Services.Supplements
{
    using System.Collections.Generic;
    using Models;
    using StreetWorkout.ViewModels.Supplements;

    public interface ISupplementService
    {
        void Create(string name, int categoryId, string imageUrl, string content, decimal price, short quantity);

        IEnumerable<SupplementCategoryViewModel> GetSupplementCategories();

        SupplementsQueryModel All(int currentPage);

        SupplementServiceModel Details(int id);

        bool IsValidCategoryId(int id);
    }
}
