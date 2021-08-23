namespace StreetWorkout
{
    public class WebConstants
    {
        public const string SystemName = "StreetWorkout";
        public const string AreaName = "Administration";
        public const string AdministratorRoleName = "Administrator";

        public class TempDataMessageKeys
        {
            public const string EditKey = "EditKey";
            public const string EditMessage = "You Edited {0} Successfully.";

            public const string DeleteKey = "DeleteKey";
            public const string DeleteMessage = "You Deleted {0} Successfully.";

            public const string NotFoundDataKey = "NotFoundData";
            public const string NotFoundDataMessage = "Not Found {0} with given searched criteria.";

            public const string RestoreKey = "RestoreKey";
            public const string RestoreMessage = "You Restored {0} Successfully.";
        }

        public class ModelStateMessage
        {
            public const string InvalidSport = "Invalid Sport.";
            public const string InvalidGoal = "Invalid Goal.";
            public const string InvalidBodyPart = "Invalid body part.";
            public const string InvalidTrainingFrequency = "Invalid Training Frequency.";
            public const string InvalidDifficultLevel = "Invalid difficult level.";
            public const string InvalidGenderType = "Invalid Gender type.";
            public const string InvalidSupplementCategory = "Invalid category.";

            public const string CannotEditAccount = "Cannot edit this account because is not completed.";

            public const string InvalidStartWorkoutTime = "Start Workout Time must be bigger than datetime now. Also End Time must be bigger than the Start Time.";
            public const string InvalidEndWorkoutTime = "End Workout Time must be bigger than datetime now. Also End Time must be bigger than the Start Time.";
        }
    }
}
