namespace StreetWorkout.Infrastructure
{
    using AutoMapper;
    using Data.Models;
    using ViewModels.Workouts;
    using ViewModels.Accounts;
    using Services.Homes.Models;

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
            this.CreateMap<Workout, WorkoutFormModel>();
        }
    }
}
