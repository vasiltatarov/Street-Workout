namespace StreetWorkout.Services
{
    using Data;
    using Data.Models;

    public class AccountService : IAccountService
    {
        private readonly StreetWorkoutDbContext data;

        public AccountService(StreetWorkoutDbContext data)
            => this.data = data;

        public bool IsUserAccountComplete(string id)
            => this.data.Users.Find(id).IsAccountCompleted;

        public void CompleteAccount(string userId, int sportId, int goalId, int trainingFrequency, int weight, int height,
            string description)
        {
            var userData = new UserData
            {
                UserId = userId,
                SportId = sportId,
                GoalId = goalId,
                TrainingFrequencyId = trainingFrequency,
                Weight = weight,
                Height = height,
                Description = description,
            };

            this.data.UserDatas.Add(userData);

            var user = this.data.Users.Find(userId);
            user.IsAccountCompleted = true;

            this.data.SaveChanges();
        }
    }
}
