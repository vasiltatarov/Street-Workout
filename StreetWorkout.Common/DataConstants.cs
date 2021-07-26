namespace StreetWorkout.Common
{
    public class DataConstants
    {
        public const int UsernameMinLength = 3;
        public const int UsernameMaxLength = 35;

        public const int DescriptionMinLength = 10;
        public const int DescriptionMaxLength = 2000;

        public const int CityMinLength = 5;
        public const int CityMaxLength = 40;

        public const int CountryNameMaxLength = 50;

        public const int WeightMinValue = 20;
        public const int WeightMaxValue = 250;

        public const int HeightMinValue = 50;
        public const int HeightMaxValue = 230;
        
        public const int GoalNameMaxLength = 40;

        public const int SportNameMaxLength = 30;

        public const int TrainingFrequencyNameMaxLength = 35;

        public const int BodyPartNameMaxLength = 40;

        public const int CreateWorkoutFormModelMinutesMinValue = 5;
        public const int CreateWorkoutFormModelMinutesMaxValue = 150;

        public const int WorkoutTitleMinLength = 10;
        public const int WorkoutTitleMaxLength = 200;
    }

    public class GroupWorkoutConstants
    {
        public const int TitleMinLength = 10;
        public const int TitleMaxLength = 200;

        public const int AddressMinLength = 10;
        public const int AddressMaxLength = 150;

        public const int MaximumParticipantsMinValue = 10;
        public const int MaximumParticipantsMaxValue = 500;

        public const int PricePerPersonMinValue = 5;
        public const int PricePerPersonMaxValue = 100;
    }
}
