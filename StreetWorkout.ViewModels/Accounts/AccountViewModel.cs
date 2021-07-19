﻿namespace StreetWorkout.ViewModels.Accounts
{
    public class AccountViewModel
    {
        public string Username { get; set; }

        public string ImageUrl { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public int Age { get; set; }

        public bool IsTrainer { get; set; }

        public bool IsAccountComplete { get; set; }

        public AccountUserDataViewModel Data { get; set; }
    }
}
