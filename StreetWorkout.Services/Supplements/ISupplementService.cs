namespace StreetWorkout.Services.Supplements
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Models;
    using StreetWorkout.ViewModels.Supplements;

    public interface ISupplementService
    {
        Task Create(string name, int categoryId, string imageUrl, string content, decimal price, short quantity);

        Task<IEnumerable<SupplementCategoryViewModel>> GetSupplementCategories();

        Task<SupplementsQueryModel> All(int currentPage, string searchTerms, int categoryId);

        Task<SupplementServiceModel> Details(int id);

        Task<SupplementFormModel> EditForModel(int id);

        Task<T> BuySupplementModel<T>(int id);

        Task<bool> Edit(int id, string name, int categoryId, string imageUrl, string content, decimal price, short quantity);

        Task<bool> Delete(int id);

        Task<bool> IsValidCategoryId(int id);

        Task<bool> IsValidWorkoutId(int id);
    }
}
