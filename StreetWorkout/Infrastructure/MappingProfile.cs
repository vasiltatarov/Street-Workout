using System;

namespace StreetWorkout.Infrastructure
{
    using AutoMapper;
    using Services.Homes.Models;
    using Services.Supplements.Models;
    using ViewModels.Workouts;
    using ViewModels.Accounts;
    using ViewModels.GroupWorkouts;
    using ViewModels.Supplements;
    using Data.Models;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // From Services
            this.CreateMap<UserData, UserIndexServiceModel>()
                .ForMember(x => x.Country,
                    y => y.MapFrom(x => x.User.Country.Name))
                .ForMember(x => x.Gender,
                    y => y.MapFrom(x => x.User.Gender))
                .ForMember(x => x.Age,
                    y => y.MapFrom(x => DateTime.Now.Year - x.User.DateOfBirth.Year));
            this.CreateMap<Supplement, SupplementServiceModel>()
                .ForMember(x => x.Category,
                y => y.MapFrom(x => x.Category.Name));

            // From View Models
            this.CreateMap<Sport, SportViewModel>();
            this.CreateMap<Goal, GoalInAccountViewModel>();
            this.CreateMap<BodyPart, BodyPartViewModel>();
            this.CreateMap<Sport, SportViewModel>();
            this.CreateMap<Workout, WorkoutFormModel>();
            this.CreateMap<GroupWorkout, GroupWorkoutFormModel>();
            this.CreateMap<SupplementCategory, SupplementCategoryViewModel>();
            this.CreateMap<Supplement, SupplementFormModel>();

        }
    }
}
