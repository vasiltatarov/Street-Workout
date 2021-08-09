namespace StreetWorkout.Services.SupplementCategories
{
    using System.Linq;
    using System.Collections.Generic;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using Data;
    using Models;

    public class SupplementCategoryService : ISupplementCategoryService
    {
        private readonly StreetWorkoutDbContext data;
        private readonly IMapper mapper;

        public SupplementCategoryService(StreetWorkoutDbContext data, IMapper mapper)
        {
            this.data = data;
            this.mapper = mapper;
        }

        public IEnumerable<SupplementCategoryServiceModel> GetAll()
            => this.data
                .SupplementCategories
                .ProjectTo<SupplementCategoryServiceModel>(this.mapper.ConfigurationProvider)
                .ToList();

        public bool Delete(int id)
        {
            var supplementCategory = this.data.SupplementCategories.FirstOrDefault(x => x.Id == id);

            if (supplementCategory == null)
            {
                return false;
            }

            supplementCategory.IsDeleted = true;
            this.data.SaveChanges();

            return true;
        }
    }
}
