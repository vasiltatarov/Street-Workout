namespace StreetWorkout.Services.GroupWorkouts
{
    using System;
    using System.Linq;

    using Data;
    using Data.Models;

    public class GroupWorkoutService : IGroupWorkoutService
    {
        private readonly StreetWorkoutDbContext data;

        public GroupWorkoutService(StreetWorkoutDbContext data)
            => this.data = data;

        public bool IsUserTrainer(string userId)
        {
            var user = this.data.Users.FirstOrDefault(x => x.Id == userId);

            if (user == null)
            {
                return false;
            }

            return user.UserRole == UserRole.Trainer;
        }

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
            };
            this.data.GroupWorkouts.Add(groupWorkout);
            this.data.SaveChanges();
        }

        public GroupWorkoutsQueryModel All(int currentPage, string userId)
        {
            var workoutsQuery = this.data.GroupWorkouts.AsQueryable();

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

        public static string GetImageBySport(string sport)
            => sport switch
            {
                "Street Workout/Calisthenics" => "https://www.grenoble.fr/uploads/Image/d3/IMF_100/GAB_GRENOBLE2019/7564_833_Street-workout-des-Allies.jpg",
                _ => "",
            };
    }
}
