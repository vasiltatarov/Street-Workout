namespace StreetWorkout.Infrastructure
{
    using System;
    using AutoMapper;

    using Data.Models;
    using Services.Homes.Models;
    using Services.Supplements.Models;
    using Services.SupplementCategories.Models;
    using Services.WorkoutPayments.Models;
    using ViewModels.Workouts;
    using ViewModels.Accounts;
    using ViewModels.GroupWorkouts;
    using ViewModels.Supplements;
    using ViewModels.Chat;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // From Services
            this.CreateMap<ApplicationUser, UserIndexServiceModel>()
                .ForMember(x => x.Country,
                    y => y.MapFrom(x => x.Country.Name))
                .ForMember(x => x.Gender,
                    y => y.MapFrom(x => x.Gender))
                .ForMember(x => x.UserRole,
                    y => y.MapFrom(x => x.UserRole))
                .ForMember(x => x.Age,
                    y => y.MapFrom(x => DateTime.Now.Year - x.DateOfBirth.Year));
            this.CreateMap<Supplement, SupplementServiceModel>()
                .ForMember(x => x.Category,
                y => y.MapFrom(x => x.Category.Name));
            this.CreateMap<SupplementCategory, SupplementCategoryServiceModel>();
            this.CreateMap<SupplementCategory, SupplementCategoryEditServiceModel>();
            this.CreateMap<UserWorkoutPayment, UserWorkoutPaymentServiceModel>()
                .ForMember(x => x.User,
                    y => y.MapFrom(x => x.User.UserName))
                .ForMember(x => x.GroupWorkout,
                    y => y.MapFrom(x => x.GroupWorkout.Title))
                .ForMember(x => x.TicketPrice,
                    y => y.MapFrom(x => x.GroupWorkout.PricePerPerson));

            // From View Models
            this.CreateMap<Sport, SportViewModel>();
            this.CreateMap<Goal, GoalInAccountViewModel>();
            this.CreateMap<BodyPart, BodyPartViewModel>();
            this.CreateMap<Sport, SportViewModel>();
            this.CreateMap<Workout, WorkoutFormModel>();
            this.CreateMap<GroupWorkout, GroupWorkoutFormModel>();
            this.CreateMap<SupplementCategory, SupplementCategoryViewModel>();
            this.CreateMap<Supplement, SupplementFormModel>();
            this.CreateMap<ChatMessage, Message>()
                .ForMember(x => x.Username,
                    y => y.MapFrom(x => x.User.UserName))
                .ForMember(x => x.UserImageUrl,
                    y => y.MapFrom(x => x.User.ImageUrl));
        }
    }
}
