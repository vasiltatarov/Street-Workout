namespace StreetWorkout.Services
{
    public interface IAccountService
    {
        bool IsUserAccountComplete(string id);

        void CompleteAccount(string userId, int sportId, int goalId, int trainingFrequency, int weight, int height, string description);
    }
}
