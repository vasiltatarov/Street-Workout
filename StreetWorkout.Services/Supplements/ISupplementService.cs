namespace StreetWorkout.Services.Supplements
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using StreetWorkout.Services.Supplements.Models;
    using StreetWorkout.ViewModels.Supplements;

    public interface ISupplementService
    {
        Task Create(string name, int categoryId, string imageUrl, string content, decimal price, short quantity);

        Task BuySupplement(int supplementId, string userId, string firstName, string lastName, string phone, string email, string address, string cardName, string cardNumber, string expiration);

        Task<IEnumerable<SupplementCategoryViewModel>> GetSupplementCategories();

        Task<SupplementsQueryModel> All(int currentPage, string searchTerms, int categoryId);

        Task<SupplementServiceModel> Details(int id);

        Task<SupplementFormModel> EditForModel(int id);

        Task<T> BuySupplementModel<T>(int id);

        Task<bool> Edit(int id, string name, int categoryId, string imageUrl, string content, decimal price, short quantity);

        Task<bool> Delete(int id);

        Task<bool> IsValidCategoryId(int id);

        Task<bool> IsValidSupplementId(int id);
    }
}
