namespace StreetWorkout.Infrastructure
{
    using AutoMapper;
    using Data.Models;
    using Services.Homes;
    using Services.Workouts;
    using ViewModels.Workouts;
    using ViewModels.Accounts;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // From Services
            this.CreateMap<UserData, UserIndexServiceModel>();

            // From View Models
            this.CreateMap<Sport, SportViewModel>();
            this.CreateMap<Goal, GoalInAccountViewModel>();
            this.CreateMap<BodyPart, BodyPartInCreateWorkoutViewModel>();
            this.CreateMap<Sport, SportViewModel>();
        }
    }
}
