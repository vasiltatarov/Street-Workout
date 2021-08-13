namespace StreetWorkout.Services.SupplementCategories
{
    using System.Linq;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
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

        public async Task<IEnumerable<SupplementCategoryServiceModel>> GetAll()
            => await this.data
                .SupplementCategories
                .ProjectTo<SupplementCategoryServiceModel>(this.mapper.ConfigurationProvider)
                .ToListAsync();

        public async Task<bool> Delete(int id)
        {
            var supplementCategory = await this.data.SupplementCategories.FirstOrDefaultAsync(x => x.Id == id);

            if (supplementCategory == null)
            {
                return false;
            }

            supplementCategory.IsDeleted = true;
            await this.data.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Restore(int id)
        {
            var supplementCategory = await this.data.SupplementCategories.FirstOrDefaultAsync(x => x.Id == id);

            if (supplementCategory == null)
            {
                return false;
            }

            if (!supplementCategory.IsDeleted)
            {
                return true;
            }

            supplementCategory.IsDeleted = false;
            await this.data.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Edit(int id, string name)
        {
            var supplementCategory = await this.data.SupplementCategories.FirstOrDefaultAsync(x => x.Id == id);

            if (supplementCategory == null)
            {
                return false;
            }

            supplementCategory.Name = name;
            await this.data.SaveChangesAsync();

            return true;
        }

        public async Task<SupplementCategoryEditServiceModel> GetSupplementCategoryEditModel(int id)
            => await this.data
                .SupplementCategories
                .Where(x => x.Id == id)
                .ProjectTo<SupplementCategoryEditServiceModel>(this.mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
    }
}
