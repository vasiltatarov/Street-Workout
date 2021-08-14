namespace StreetWorkout.Data
{
    public class DataConstants
    {
        public const int DescriptionMaxLength = 2000;

        public const int CityMinLength = 5;
        public const int CityMaxLength = 40;

        public const int CountryNameMaxLength = 50;
        
        public const int WeightMaxValue = 250;
        
        public const int HeightMaxValue = 230;
        
        public const int GoalNameMaxLength = 40;

        public const int SportNameMaxLength = 30;

        public const int TrainingFrequencyNameMaxLength = 35;

        public const int BodyPartNameMaxLength = 40;
        
        public const int CreateWorkoutFormModelMinutesMaxValue = 150;
        
        public const int WorkoutTitleMaxLength = 200;

        public const int CommentContentMaxLength = 1000;

        public const int UsernameMinLength = 3;
        public const int UsernameMaxLength = 35;

        public class UserWorkoutPaymentConstants
        {
            public const int FullNameMaxLength = 40;
        }

        public class GroupWorkoutConstants
        {
            public const int TitleMaxLength = 200;

            public const int AddressMaxLength = 150;

            public const int MaximumParticipantsMaxValue = 500;

            public const int PricePerPersonMaxValue = 100;
        }

        public class SupplementConstants
        {
            public const int NameMaxLength = 80;
        }

        public class SupplementCategoryConstants
        {
            public const int SupplementCategoryNameMaxLength = 30;
        }
        public class ChatMessageConstants
        {
            public const int TextMaxLength = 300;
        }

        public class PaymentConstants
        {
            public const int FirstNameMaxLength = 30;
            public const int LastNameMaxLength = 30;
            public const int AddressMaxLength = 100;
            public const int CardNameMaxLength = 30;
            public const int CardNumberMaxLength = 20;
            public const int ExpirationMaxLength = 10;
        }
    }
}
