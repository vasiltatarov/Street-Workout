namespace StreetWorkout.Services.SupplementCategories
{
    using System.Collections.Generic;
    using Models;

    public interface ISupplementCategoryService
    {
        IEnumerable<SupplementCategoryServiceModel> GetAll();

        bool Delete(int id);

        bool Restore(int id);

        bool Edit(int id, string name);

        SupplementCategoryEditServiceModel GetSupplementCategoryEditModel(int id);
    }
}
