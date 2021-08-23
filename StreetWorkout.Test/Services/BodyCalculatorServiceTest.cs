namespace StreetWorkout.Test.Services
{
    using StreetWorkout.Data.Models.Enums;
    using StreetWorkout.Services.BodyCalculators;
    using Xunit;

    public class BodyCalculatorServiceTest
    {
        [Fact]
        public void CalculateCaloriesShouldCalculateCaloriesAndReturnDoubleValue()
        {
            // Arrange
            var bodyCalculatorService = new BodyCalculatorService();

            // Act
            var result = bodyCalculatorService.CalculateCalories(80, 180, 1.55, 21, Gender.Male);

            // Assert
            Assert.IsType<double>(result);
            Assert.Equal(2972, result);
        }
    }
}
