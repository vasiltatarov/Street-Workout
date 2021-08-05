namespace StreetWorkout.Services.Workouts
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using StreetWorkout.ViewModels.Workouts;
    using Data;
    using Data.Models;
    using Data.Models.Enums;
    using Models;

    public class WorkoutService : IWorkoutService
    {
        private readonly StreetWorkoutDbContext data;
        private readonly IMapper mapper;

        public WorkoutService(StreetWorkoutDbContext data, IMapper mapper)
        {
            this.data = data;
            this.mapper = mapper;
        }

        public void Create(string title, int sportId, DifficultLevel difficultLevel, int bodyPartId, string userId, int minutes, string content)
        {
            var workout = new Workout
            {
                Title = title,
                SportId = sportId,
                DifficultLevel = difficultLevel,
                BodyPartId = bodyPartId,
                UserId = userId,
                Minutes = minutes,
                Content = content,
                CreatedOn = DateTime.UtcNow,
            };
            this.data.Workouts.Add(workout);
            this.data.SaveChanges();
        }

        public bool Edit(int id, string title, int sportId, DifficultLevel difficultLevel, int bodyPartId, int minutes, string content)
        {
            var workout = this.data.Workouts.FirstOrDefault(x => x.Id == id);

            if (workout == null)
            {
                return false;
            }

            workout.Title = title;
            workout.SportId = sportId;
            workout.DifficultLevel = difficultLevel;
            workout.BodyPartId = bodyPartId;
            workout.Minutes = minutes;
            workout.Content = content;

            this.data.SaveChanges();

            return true;
        }

        public WorkoutsQueryModel Workouts(string userId, string sport, string bodyPart, string searchTerms, int currentPage)
        {
            var workoutsQuery = this.data
                .Workouts
                .Where(x => !x.IsDeleted)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(sport))
            {
                workoutsQuery = workoutsQuery.Where(x => x.Sport.Name.ToLower() == sport.ToLower());
            }

            if (!string.IsNullOrWhiteSpace(bodyPart))
            {
                workoutsQuery = workoutsQuery.Where(x => x.BodyPart.Name.ToLower() == bodyPart.ToLower());
            }

            if (!string.IsNullOrWhiteSpace(searchTerms))
            {
                workoutsQuery = workoutsQuery
                    .Where(x => x.Title.ToLower().Contains(searchTerms.ToLower()) ||
                                x.Content.ToLower().Contains(searchTerms.ToLower()));
            }

            var workouts = workoutsQuery
                .OrderByDescending(x => x.Id)
                .Skip((currentPage - 1) * WorkoutsQueryModel.WorkoutsPerPage)
                .Take(WorkoutsQueryModel.WorkoutsPerPage)
                .Select(x => new WorkoutServiceModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Sport = x.Sport.Name,
                    DifficultLevel = x.DifficultLevel.ToString(),
                    BodyPart = x.BodyPart.Name,
                    ImageUrl = GetImage(x.BodyPart.Name),
                    Minutes = x.Minutes,
                });

            var sports = this.data
                .Sports
                .Select(x => x.Name)
                .ToList();

            var bodyParts = this.data
                .BodyParts
                .Select(x => x.Name)
                .ToList();

            var totalWorkouts = workoutsQuery.Count();

            return new()
            {
                IsUserTrainer = this.data.Users.Find(userId).UserRole == UserRole.Trainer,
                Workouts = workouts,
                CurrentPage = currentPage,
                TotalWorkouts = totalWorkouts,
                Sport = sport,
                BodyPart = bodyPart,
                Sports = sports,
                BodyParts = bodyParts,
                SearchTerms = searchTerms,
            };
        }

        public WorkoutDetailsServiceModel Details(int id)
            => this.data
                .Workouts
                .Where(x => x.Id == id)
                .Select(x => new WorkoutDetailsServiceModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Sport = x.Sport.Name,
                    DifficultLevel = x.DifficultLevel.ToString(),
                    BodyPart = x.BodyPart.Name,
                    ImageUrl = GetImage(x.BodyPart.Name),
                    UserUsername = x.User.UserName,
                    UserImageUrl = x.User.ImageUrl,
                    UserDescription = this.data
                        .UserDatas
                        .FirstOrDefault(ud => ud.UserId == x.UserId)
                        .Description,
                    Minutes = x.Minutes,
                    Content = x.Content,
                    CreatedOn = x.CreatedOn,
                    Sports = this.data
                        .Sports
                        .Select(s => s.Name)
                        .OrderBy(s => s)
                        .ToList(),
                    LatestWorkouts = this.data
                        .Workouts
                        .OrderByDescending(w => w.Id)
                        .Take(3)
                        .Select(w => new WorkoutDetailsLatestTraining
                        {
                            Id = w.Id,
                            Title = w.Title,
                            ImageUrl = GetImage(w.BodyPart.Name),
                            CreatedOn = w.CreatedOn,
                        })
                        .ToList(),
                    Comments = this.data
                        .Comments
                        .Where(c => c.WorkoutId == id)
                        .Select(c => new CommentInDetailsServiceModel
                        {
                            Id = c.Id,
                            UserUserName = c.User.UserName,
                            Content = c.Content,
                            UserImageUrl = c.User.ImageUrl,
                        })
                        .OrderByDescending(c => c.Id)
                        .ToList(),
                })
                .FirstOrDefault();

        public WorkoutFormModel EditFormModel(int id)
            => this.data
                .Workouts
                .Where(x => x.Id == id)
                .ProjectTo<WorkoutFormModel>(this.mapper.ConfigurationProvider)
                .FirstOrDefault();

        public IEnumerable<SportViewModel> GetSports()
            => this.data.Sports
                .ProjectTo<SportViewModel>(this.mapper.ConfigurationProvider)
                .ToList();

        public IEnumerable<BodyPartViewModel> GetBodyParts()
            => this.data.BodyParts
                .ProjectTo<BodyPartViewModel>(this.mapper.ConfigurationProvider)
                .ToList();

        public bool IsUserCreator(string userId, int workoutId)
            => this.data
                .Workouts
                .Any(x => x.Id == workoutId && x.UserId == userId);

        public bool IsValidSportId(int id)
            => this.data.Sports.Any(x => x.Id == id);

        public bool IsValidBodyPartId(int id)
            => this.data.BodyParts.Any(x => x.Id == id);

        public bool Delete(int id)
        {
            var workout = this.data.Workouts.FirstOrDefault(x => x.Id == id);

            if (workout == null)
            {
                return false;
            }

            workout.IsDeleted = true;
            this.data.SaveChanges();

            return true;
        }

        private static string GetImage(string bodyPart)
            => bodyPart switch
            {
                "Upper Body" => "https://cdna.artstation.com/p/assets/images/images/000/130/718/large/bhunesh-ramwani-uper-body.jpg?1405094263",
                "Lower Body" => "https://i.ytimg.com/vi/I9nG-G4B5Bs/maxresdefault.jpg",
                "Full Body" => "https://builtwithscience.com/wp-content/uploads/2019/01/full-body-workout-A-thumbnail-min.jpg",
                "Back" => "https://bestlifeonline.com/wp-content/uploads/2017/04/shutterstock_272601662-1024x682.jpg",
                "Chest" => "https://i.ytimg.com/vi/lWXhih3xbVc/maxresdefault.jpg",
                "Forearms" => "https://i.ytimg.com/vi/0XS0j1Gtobw/maxresdefault.jpg",
                "Triceps" => "https://builtwithscience.com/wp-content/uploads/2018/08/best-tricep-exercises.png",
                "Arms" => "https://bonytobeastly.com/wp-content/uploads/2021/05/arm-muscle-anatomy-for-bodybuilding-1.jpg",
                "Biceps" => "https://athleanx.com/wp-content/uploads/2018/04/biceps-muscles-anatomy-brachialis-long-head-short-head.jpg",
                "Shoulders" => "https://i.ytimg.com/vi/2Vprklw8cu8/maxresdefault.jpg",
                "Legs" => "https://i.ytimg.com/vi/RjexvOAsVtI/hqdefault.jpg",
                "ABS" => "https://i.pinimg.com/originals/63/37/aa/6337aa509ce4faa748ba48f383e67a80.jpg",
                "Neck" => "https://i.pinimg.com/originals/27/e2/c4/27e2c4879437c4420b9b8dc4ad991d91.jpg",
                _ => "https://i.pinimg.com/originals/0c/4b/34/0c4b3494a30b15368539140920cbf2ed.png",
            };
    }
}
