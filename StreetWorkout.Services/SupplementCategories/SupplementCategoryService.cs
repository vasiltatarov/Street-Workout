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

        public bool Restore(int id)
        {
            var supplementCategory = this.data.SupplementCategories.FirstOrDefault(x => x.Id == id);

            if (supplementCategory == null)
            {
                return false;
            }

            if (!supplementCategory.IsDeleted)
            {
                return true;
            }

            supplementCategory.IsDeleted = false;
            this.data.SaveChanges();

            return true;
        }

        public bool Edit(int id, string name)
        {
            var supplementCategory = this.data.SupplementCategories.FirstOrDefault(x => x.Id == id);

            if (supplementCategory == null)
            {
                return false;
            }

            supplementCategory.Name = name;
            this.data.SaveChanges();

            return true;
        }

        public SupplementCategoryEditServiceModel GetSupplementCategoryEditModel(int id)
            => this.data
                .SupplementCategories
                .Where(x => x.Id == id)
                .ProjectTo<SupplementCategoryEditServiceModel>(this.mapper.ConfigurationProvider)
                .FirstOrDefault();
    }
}
