namespace StreetWorkout.Services.SupplementCategories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;

    public interface ISupplementCategoryService
    {
        Task<IEnumerable<SupplementCategoryServiceModel>> GetAll();

        Task<bool> Delete(int id);

        Task<bool> Restore(int id);

        Task<bool> Edit(int id, string name);

        Task<SupplementCategoryEditServiceModel> GetSupplementCategoryEditModel(int id);
    }
}
