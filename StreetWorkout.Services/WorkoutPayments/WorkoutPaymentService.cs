namespace StreetWorkout.Services.WorkoutPayments
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Microsoft.EntityFrameworkCore;
    using StreetWorkout.Data;
    using StreetWorkout.Services.WorkoutPayments.Models;

    public class WorkoutPaymentService : IWorkoutPaymentService
    {
        private readonly StreetWorkoutDbContext data;
        private readonly IMapper mapper;

        public WorkoutPaymentService(StreetWorkoutDbContext data, IMapper mapper)
        {
            this.data = data;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<UserWorkoutPaymentServiceModel>> All()
            => await this.data
                .UserWorkoutPayments
                .OrderByDescending(x => x.Id)
                .ProjectTo<UserWorkoutPaymentServiceModel>(this.mapper.ConfigurationProvider)
                .ToListAsync();
    }
}
