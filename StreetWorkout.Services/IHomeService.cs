namespace StreetWorkout.Services
{
    using ViewModels.Home;

    public interface IHomeService
    {
        IndexViewModel IndexViewModel(string userId);
    }
}
