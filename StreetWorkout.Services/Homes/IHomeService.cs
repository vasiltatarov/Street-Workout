namespace StreetWorkout.Services.Homes
{
    using ViewModels.Home;

    public interface IHomeService
    {
        IndexViewModel IndexViewModel(string userId);
    }
}
