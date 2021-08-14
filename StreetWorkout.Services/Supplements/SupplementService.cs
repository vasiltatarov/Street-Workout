namespace StreetWorkout.Services.Supplements
{
    using System.Threading.Tasks;
    using System.Linq;
    using System.Collections.Generic;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Microsoft.EntityFrameworkCore;

    using StreetWorkout.ViewModels.Supplements;
    using Models;
    using Data;
    using Data.Models;

    public class SupplementService : ISupplementService
    {
        private readonly StreetWorkoutDbContext data;
        private readonly IMapper mapper;

        public SupplementService(StreetWorkoutDbContext data, IMapper mapper)
        {
            this.data = data;
            this.mapper = mapper;
        }

        public async Task<SupplementsQueryModel> All(int currentPage, string searchTerms, int categoryId)
        {
            var supplementsQuery = this.data
                .Supplements
                .Where(x => !x.IsDeleted)
                .AsQueryable();

            if (categoryId != 0)
            {
                supplementsQuery = supplementsQuery
                    .Where(x => x.CategoryId == categoryId)
                    .AsQueryable();
            }

            if (!string.IsNullOrWhiteSpace(searchTerms))
            {
                var searchTermsToLower = searchTerms.ToLower();

                supplementsQuery = supplementsQuery
                    .Where(x => x.Name.ToLower().Contains(searchTermsToLower)
                                || x.Content.ToLower().Contains(searchTermsToLower))
                    .AsQueryable();
            }

            var supplements = await supplementsQuery
                .Skip((currentPage - 1) * SupplementsQueryModel.SupplementsPerPage)
                .Take(SupplementsQueryModel.SupplementsPerPage)
                .Select(x => new SupplementServiceModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Category = x.Category.Name,
                    ImageUrl = x.ImageUrl,
                    Content = x.Content.Substring(0, 300),
                    Price = x.Price,
                    Quantity = x.Quantity,
                })
                .ToListAsync();

            return new SupplementsQueryModel
            {
                CurrentPage = currentPage,
                Supplements = supplements,
                TotalSupplements = supplementsQuery.Count(),
                Categories = await this.GetSupplementCategories(),
                CategoryId = categoryId,
                SearchTerms = searchTerms,
            };
        }

        public async Task<SupplementServiceModel> Details(int id)
            => await this.data
                .Supplements
                .Where(x => x.Id == id)
                .ProjectTo<SupplementServiceModel>(this.mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

        public async Task<SupplementFormModel> EditForModel(int id)
            => await this.data
                .Supplements
                .Where(x => x.Id == id)
                .ProjectTo<SupplementFormModel>(this.mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

        public async Task<T> BuySupplementModel<T>(int id)
            => await this.data
                .Supplements
                .Where(x => x.Id == id)
                .ProjectTo<T>(this.mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

        public async Task<bool> Edit(int id, string name, int categoryId, string imageUrl, string content, decimal price, short quantity)
        {
            var supplement = await this.data.Supplements.FirstOrDefaultAsync(x => x.Id == id);

            if (supplement == null)
            {
                return false;
            }

            supplement.Name = name;
            supplement.CategoryId = categoryId;
            supplement.ImageUrl = imageUrl;
            supplement.Content = content;
            supplement.Price = price;
            supplement.Quantity = quantity;

            await this.data.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Delete(int id)
        {
            var supplement = await this.data.Supplements.FirstOrDefaultAsync(x => x.Id == id);

            if (supplement == null)
            {
                return false;
            }

            supplement.IsDeleted = true;

            await this.data.SaveChangesAsync();

            return true;
        }

        public async Task Create(string name, int categoryId, string imageUrl, string content, decimal price, short quantity)
        {
            await this.data.Supplements.AddAsync(new Supplement
            {
                Name = name,
                CategoryId = categoryId,
                ImageUrl = imageUrl,
                Content = content,
                Price = price,
                Quantity = quantity,
            });
            await this.data.SaveChangesAsync();
        }

        public async Task<IEnumerable<SupplementCategoryViewModel>> GetSupplementCategories()
            => await this.data
                .SupplementCategories
                .Where(x => !x.IsDeleted)
                .ProjectTo<SupplementCategoryViewModel>(this.mapper.ConfigurationProvider)
                .ToListAsync();

        public async Task<bool> IsValidCategoryId(int id)
            => await this.data
                .SupplementCategories
                .AnyAsync(x => x.Id == id);

        public async Task<bool> IsValidSupplementId(int id)
            => await this.data
                .Workouts
                .AnyAsync(x => x.Id == id);

        public async Task BuySupplement(int supplementId, string userId, string firstName, string lastName, string phone, string email,
            string address, string cardName, string cardNumber, string expiration)
        {
            await this.data.SupplementPayments.AddAsync(new SupplementPayment
            {
                SupplementId = supplementId,
                Payment = new Payment
                {
                    UserId = userId,
                    FirstName = firstName,
                    LastName = lastName,
                    PhoneNumber = phone,
                    Email = email,
                    Address = address,
                    CardName = cardName,
                    CardNumber = cardNumber,
                    Expiration = expiration,
                }
            });
            await this.data.SaveChangesAsync();
        }
    }
}
