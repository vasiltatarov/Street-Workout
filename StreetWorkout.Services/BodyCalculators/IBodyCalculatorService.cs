namespace StreetWorkout.Services.BodyCalculators
{
    using StreetWorkout.Data.Models.Enums;

    public interface IBodyCalculatorService
    {
        double CalculateCalories(byte weight, byte height, double activity, byte age, Gender gender);
    }
}
