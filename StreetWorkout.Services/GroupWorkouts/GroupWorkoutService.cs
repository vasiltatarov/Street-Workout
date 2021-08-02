namespace StreetWorkout.Services.GroupWorkouts
{
    using System;
    using System.Linq;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using Data;
    using Data.Models;
    using Data.Models.Enums;
    using Workouts.Models;
    using Models;
    using StreetWorkout.ViewModels.GroupWorkouts;

    public class GroupWorkoutService : IGroupWorkoutService
    {
        private readonly StreetWorkoutDbContext data;
        private readonly IMapper mapper;

        public GroupWorkoutService(StreetWorkoutDbContext data, IMapper mapper)
        {
            this.data = data;
            this.mapper = mapper;
        }

        public bool IsUserTrainer(string userId)
        {
            var user = this.data.Users.FirstOrDefault(x => x.Id == userId);

            if (user == null)
            {
                return false;
            }

            return user.UserRole == UserRole.Trainer;
        }

        public bool IsUserCreator(string userId, int groupWorkoutId)
            => this.data
                .GroupWorkouts
                .Any(x => x.TrainerId == userId && x.Id == groupWorkoutId);

        public void Create(string title, int sportId, string address, DateTime startOn, DateTime endOn, byte maximumParticipants, byte pricePerPerson, string trainerId, string content)
        {
            var groupWorkout = new GroupWorkout
            {
                Title = title,
                SportId = sportId,
                Address = address,
                StartOn = startOn,
                EndOn = endOn,
                MaximumParticipants = maximumParticipants,
                PricePerPerson = pricePerPerson,
                TrainerId = trainerId,
                Content = content,
                CreatedOn = DateTime.UtcNow,
            };
            this.data.GroupWorkouts.Add(groupWorkout);
            this.data.SaveChanges();
        }

        public bool Edit(int id, string title, int sportId, string address, DateTime startOn, DateTime endOn, byte maximumParticipants, byte pricePerPerson, string content)
        {
            var groupWorkout = this.data.GroupWorkouts.FirstOrDefault(x => x.Id == id);

            if (groupWorkout == null)
            {
                return false;
            }

            groupWorkout.Title = title;
            groupWorkout.SportId = sportId;
            groupWorkout.Address = address;
            groupWorkout.StartOn = startOn;
            groupWorkout.EndOn = endOn;
            groupWorkout.MaximumParticipants = maximumParticipants;
            groupWorkout.PricePerPerson = pricePerPerson;
            groupWorkout.Content = content;
            this.data.SaveChanges();

            return true;
        }

        public bool Delete(int id)
        {
            var groupWorkout = this.data.GroupWorkouts.FirstOrDefault(x => x.Id == id);

            if (groupWorkout == null)
            {
                return false;
            }

            groupWorkout.IsDeleted = true;
            this.data.SaveChanges();

            return true;
        }

        public byte AvailableTickets(int groupWorkoutId)
        {
            var groupWorkout = this.data.GroupWorkouts.FirstOrDefault(x => x.Id == groupWorkoutId);

            if (groupWorkout == null)
            {
                return 0;
            }

            var maxPlaces = groupWorkout.MaximumParticipants;

            var boughtTickets = this.data
                .UserWorkoutPayments
                .Where(x => x.GroupWorkoutId == groupWorkoutId)
                .Sum(x => x.BoughtTickets);

            return (byte)(maxPlaces - boughtTickets);
        }

        public void BuyTicket(string userId, int groupWorkoutId, string fullName, string phoneNumber, string card, byte boughtTickets)
        {
            var userWorkoutPayment = new UserWorkoutPayment
            {
                UserId = userId,
                GroupWorkoutId = groupWorkoutId,
                FullName = fullName,
                PhoneNumber = phoneNumber,
                Card = card,
                BoughtTickets = boughtTickets,
                CreatedOn = DateTime.UtcNow,
            };
            this.data.UserWorkoutPayments.Add(userWorkoutPayment);
            this.data.SaveChanges();
        }

        public GroupWorkoutsQueryModel All(int currentPage, string userId)
        {
            var workoutsQuery = this.data.GroupWorkouts
                .Where(x => !x.IsDeleted)
                .AsQueryable();

            var groupWorkouts = workoutsQuery
                .Skip((currentPage - 1) * GroupWorkoutsQueryModel.WorkoutsPerPage)
                .Take(GroupWorkoutsQueryModel.WorkoutsPerPage)
                .Select(x => new GroupWorkoutModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Address = x.Address,
                    MaximumParticipants = x.MaximumParticipants,
                    PricePerPerson = x.PricePerPerson,
                    Sport = x.Sport.Name,
                    StartOn = x.StartOn.ToString("g"),
                    ImageUrl = GetImageBySport(x.Sport.Name),
                })
                .ToList();

            return new GroupWorkoutsQueryModel
            {
                IsUserTrainer = this.IsUserTrainer(userId),
                CurrentPage = currentPage,
                GroupWorkouts = groupWorkouts,
                TotalGroupWorkouts = groupWorkouts.Count,
            };
        }

        public GroupWorkoutDetailsModel Details(int id)
            => this.data
                .GroupWorkouts
                .Where(x => x.Id == id)
                .Select(x => new GroupWorkoutDetailsModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Address = x.Address,
                    ImageUrl = GetImageBySport(x.Sport.Name),
                    MaximumParticipants = x.MaximumParticipants,
                    PricePerPerson = x.PricePerPerson,
                    Sport = x.Sport.Name,
                    Content = x.Content,
                    TrainerUsername = x.Trainer.UserName,
                    TrainerImageUrl = x.Trainer.ImageUrl,
                    TrainerDescription = this.data
                        .UserDatas
                        .Where(ud => ud.UserId == x.TrainerId)
                        .Select(ud => ud.Description)
                        .FirstOrDefault(),
                    StartOn = x.StartOn,
                    EndOn = x.EndOn,
                    CreatedOn = x.CreatedOn.ToString("D"),
                    AvailableTickets = AvailableTickets(id),
                    LatestWorkouts = this.data
                        .Workouts
                        .OrderByDescending(w => w.Id)
                        .Take(3)
                        .Select(w => new WorkoutDetailsLatestTraining
                        {
                            Id = w.Id,
                            Title = w.Title,
                            ImageUrl = GetImageBySport(w.Sport.Name),
                            CreatedOn = w.CreatedOn,
                        })
                        .ToList(),
                })
                .FirstOrDefault();

        public GroupWorkoutFormModel EditFormModel(int id)
            => this.data
                .GroupWorkouts
                .Where(x => x.Id == id)
                .ProjectTo<GroupWorkoutFormModel>(this.mapper.ConfigurationProvider)
                .FirstOrDefault();

        public static string GetImageBySport(string sport)
            => sport switch
            {
                "Street Workout/Calisthenics" => "https://www.grenoble.fr/uploads/Image/d3/IMF_100/GAB_GRENOBLE2019/7564_833_Street-workout-des-Allies.jpg",
                "Fitness" => "https://img5.goodfon.com/wallpaper/nbig/f/15/back-fitness-gym-power-pose.jpg",
                "CrossFit" => "https://www.crossfit.com/wp-content/uploads/2020/10/19092755/2018072814475780_ND_ND5_6593-1-copy-1920x1080.jpg",
                "Weightlifting" => "https://cdn.dmcl.biz/media/image/110556/o/Mohamed+Ehab+World+Championships+2017.jpg",
                "Gymnastics" => "https://images.daznservices.com/di/library/sporting_news/6/bd/suni-lee-062721-getty-ftr_1hvg4exe4a4b31u0eybvvydgoj.jpg?t=1353181914&quality=100&w=1280&h=720",
                "Athletics" => "",
                "MMA" => "https://m.allboxing.ru/sites/default/files/ufndublin_10-mcgregor_vs_brandao_09.jpg",
                "Box" => "https://citystage.bg/wp-content/uploads/2017/09/box-sport-men-training-163403.jpeg",
                "Kick-Box" => "https://i.pinimg.com/originals/ff/0c/74/ff0c7433639e861a9d04e34f6d370a21.png",
                "Taekwondo" => "https://image.shutterstock.com/image-vector/taekwondo-vector-icon-design-illustration-260nw-1847590639.jpg",
                "Judo" => "https://78884ca60822a34fb0e6-082b8fd5551e97bc65e327988b444396.ssl.cf3.rackcdn.com/up/2019/02/ijf-03-1550476738-1550476738.jpg",
                "Karate" => "https://img.freepik.com/free-vector/martial-art-karate-logo-sport-symbol-illustration_7496-886.jpg?size=626&ext=jpg",
                _ => "",
            };
    }
}
