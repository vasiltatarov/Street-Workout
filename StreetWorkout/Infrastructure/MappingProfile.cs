namespace StreetWorkout.Infrastructure
{
    using AutoMapper;
    using Data.Models;
    using Services.Homes.Models;
    using Services.Supplements.Models;
    using ViewModels.Workouts;
    using ViewModels.Accounts;
    using ViewModels.GroupWorkouts;
    using ViewModels.Supplements;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // From Services
            this.CreateMap<UserData, UserIndexServiceModel>();
            this.CreateMap<Supplement, SupplementServiceModel>();

            // From View Models
            this.CreateMap<Sport, SportViewModel>();
            this.CreateMap<Goal, GoalInAccountViewModel>();
            this.CreateMap<BodyPart, BodyPartViewModel>();
            this.CreateMap<Sport, SportViewModel>();
            this.CreateMap<Workout, WorkoutFormModel>();
            this.CreateMap<GroupWorkout, GroupWorkoutFormModel>();
            this.CreateMap<SupplementCategory, SupplementCategoryViewModel>();
        }
    }
}
