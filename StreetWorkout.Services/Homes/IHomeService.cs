namespace StreetWorkout.Services.Homes
{
    using Models;

    public interface IHomeService
    {
        IndexServiceModel IndexViewModel(string userId);
    }
}
