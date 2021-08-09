namespace StreetWorkout.Services.SupplementCategories
{
    using System.Collections.Generic;
    using Models;

    public interface ISupplementCategoryService
    {
        IEnumerable<SupplementCategoryServiceModel> GetAll();

        bool Delete(int id);
    }
}
