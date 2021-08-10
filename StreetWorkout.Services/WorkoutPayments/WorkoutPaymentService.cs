namespace StreetWorkout.Services.WorkoutPayments
{
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Data;
    using Models;

    public class WorkoutPaymentService : IWorkoutPaymentService
    {
        private readonly StreetWorkoutDbContext data;
        private readonly IMapper mapper;

        public WorkoutPaymentService(StreetWorkoutDbContext data, IMapper mapper)
        {
            this.data = data;
            this.mapper = mapper;
        }

        public IEnumerable<UserWorkoutPaymentServiceModel> All()
            => this.data
                .UserWorkoutPayments
                .OrderByDescending(x => x.Id)
                .ProjectTo<UserWorkoutPaymentServiceModel>(this.mapper.ConfigurationProvider)
                .ToList();
    }
}
