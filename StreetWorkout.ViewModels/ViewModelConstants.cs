namespace StreetWorkout.ViewModels
{
    public class ViewModelConstants
    {
        public const int CommentInputModelWorkoutIdMinValue = 1;
        public const int CommentInputModelWorkoutIdMaxValue = 5;

        public const int CommentInputModelContentMinLength = 10;
        public const int CommentInputModelContentMaxLength = 1000;

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

        public class AccountFormModelConstants
        {
            public const int WeightMinValue = 20;
            public const int WeightMaxValue = 250;

            public const int HeightMinValue = 50;
            public const int HeightMaxValue = 230;

            public const int DescriptionMinLength = 10;
            public const int DescriptionMaxLength = 2000;
        }

        public class WorkoutFormModelConstants
        {
            public const int CreateWorkoutFormModelMinutesMinValue = 5;
            public const int CreateWorkoutFormModelMinutesMaxValue = 150;

            public const int WorkoutTitleMinLength = 10;
            public const int WorkoutTitleMaxLength = 200;
        }

        public class SupplementFormModelConstants
        {
            public const int NameMinLength = 5;
            public const int NameMaxLength = 80;

            public const int PriceMinValue = 5;
            public const int PriceMaxValue = 250;

            public const int ContentMinLength = 5;

            public const int QuantityMinValue = 200;
            public const int QuantityMaxValue = 7000;
        }
    }
}
