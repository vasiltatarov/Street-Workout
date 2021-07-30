namespace StreetWorkout.Services.BodyCalculators
{
    using Data.Models.Enums;

    public class BodyCalculatorService : IBodyCalculatorService
    {
        public double CalculateCalories(byte weight, byte height, double activity, byte age, Gender gender)
        {
            var genderValue = gender == Gender.Male ? 66 : 655.1;
            var weightInPounds = weight * 2.20462262;
            var centimetersInInches = height * 0.393700787;

            // Basal Metabolic Rate (BMR)
            double bmr;

            if (gender == Gender.Male)
            {
                bmr = genderValue + (6.2 * weightInPounds) + (12.7 * centimetersInInches) - (6.76 * age);
            }
            else
            {
                bmr = genderValue + (4.35 * weightInPounds) + (4.7 * centimetersInInches) - (4.7 * age);
            }

            var calories = (int)(bmr * activity);

            return calories;
        }
    }
}
