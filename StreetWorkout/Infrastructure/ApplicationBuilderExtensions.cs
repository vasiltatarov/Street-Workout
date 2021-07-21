namespace StreetWorkout.Infrastructure
{
    using System.Linq;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.AspNetCore.Builder;

    using Data;
    using StreetWorkout.Data.Models;

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder PrepareDatabase(this IApplicationBuilder app)
        {
            using var scopedServices = app.ApplicationServices.CreateScope();

            var data = scopedServices.ServiceProvider.GetService<StreetWorkoutDbContext>();
            data.Database.Migrate();

            SeedCountries(data);
            SeedSports(data);
            SeedGoals(data);
            SeedTrainingFrequencies(data);
            SeedBodyParts(data);

            return app;
        }

        private static void SeedBodyParts(StreetWorkoutDbContext data)
        {
            if (data.BodyParts.Any())
            {
                return;
            }

            data.BodyParts.AddRange(new BodyPart[]
            {
                new () { Name = "Upper Body" },
                new () { Name = "Lower Body" },
                new () { Name = "Full Body" },
                new () { Name = "Arms" },
                new () { Name = "Biceps" },
                new () { Name = "Triceps" },
                new () { Name = "Chest" },
                new () { Name = "Back" },
                new () { Name = "Legs" },
                new () { Name = "ABS" },
                new () { Name = "Neck" },
                new () { Name = "Shoulders" },
                new () { Name = "Forearms" },
            });
            data.SaveChanges();
        }

        private static void SeedTrainingFrequencies(StreetWorkoutDbContext data)
        {
            if (data.TrainingFrequencies.Any())
            {
                return;
            }

            data.TrainingFrequencies.AddRange(new TrainingFrequency[]
            {
                new () { Name = "Little or nothing" },
                new () { Name = "Light exercise 1 to 3 days" },
                new () { Name = "Moderate exercise 3 to 5 days" },
                new () { Name = "Strong exercise plus 5 days" },
            });

            data.SaveChanges();
        }

        private static void SeedGoals(StreetWorkoutDbContext data)
        {
            if (data.Goals.Any())
            {
                return;
            }

            data.Goals.AddRange(new Goal[]
            {
                new () { Name = "Reduced body fat" },
                new () { Name = "Building muscles" },
                new () { Name = "Gain weight" },
                new () { Name = "Lose weight" },
                new () { Name = "Toning (Just want to tone their bodies)" },
                new () { Name = "Improving endurance" },
                new () { Name = "Increasing flexibility and balance" },
            });

            data.SaveChanges();
        }

        private static void SeedSports(StreetWorkoutDbContext data)
        {
            if (data.Sports.Any())
            {
                return;
            }

            data.Sports.AddRange(new Sport[]
            {
                new () { Name = "Street Workout/Calisthenics" },
                new () { Name = "Fitness" },
                new () { Name = "CrossFit" },
                new () { Name = "Weightlifting" },
                new () { Name = "Gymnastics" },
                new () { Name = "Athletics" },
                new () { Name = "MMA" },
                new () { Name = "Box" },
                new () { Name = "Kick-Box" },
                new () { Name = "Taekwondo" },
                new () { Name = "Judo" },
                new () { Name = "Karate" },
            });

            data.SaveChanges();
        }

        private static void SeedCountries(StreetWorkoutDbContext data)
        {
            if (data.Countries.Any())
            {
                return;
            }

            var countries = new string[]
            {
                "United States", "Canada", "Afghanistan", "Albania", "Algeria", "American Samoa", "Andorra", "Angola", "Anguilla", "Antarctica", "Antigua and/or Barbuda", "Argentina", "Armenia", "Aruba", "Australia", "Austria", "Azerbaijan", "Bahamas", "Bahrain", "Bangladesh", "Barbados", "Belarus", "Belgium", "Belize", "Benin", "Bermuda", "Bhutan", "Bolivia", "Bosnia and Herzegovina", "Botswana", "Bouvet Island", "Brazil", "British Indian Ocean Territory", "Brunei Darussalam", "Bulgaria", "Burkina Faso", "Burundi", "Cambodia", "Cameroon", "Cape Verde", "Cayman Islands", "Central African Republic", "Chad", "Chile", "China", "Christmas Island", "Cocos (Keeling) Islands", "Colombia", "Comoros", "Congo", "Cook Islands", "Costa Rica", "Croatia (Hrvatska)", "Cuba", "Cyprus", "Czech Republic", "Denmark", "Djibouti", "Dominica", "Dominican Republic", "East Timor", "Ecudaor", "Egypt", "El Salvador", "Equatorial Guinea", "Eritrea", "Estonia", "Ethiopia", "Falkland Islands (Malvinas)", "Faroe Islands", "Fiji", "Finland", "France", "France, Metropolitan", "French Guiana", "French Polynesia", "French Southern Territories", "Gabon", "Gambia", "Georgia", "Germany", "Ghana", "Gibraltar", "Greece", "Greenland", "Grenada", "Guadeloupe", "Guam", "Guatemala", "Guinea", "Guinea-Bissau", "Guyana", "Haiti", "Heard and Mc Donald Islands", "Honduras", "Hong Kong", "Hungary", "Iceland", "India", "Indonesia", "Iran (Islamic Republic of)", "Iraq", "Ireland", "Israel", "Italy", "Ivory Coast", "Jamaica", "Japan", "Jordan", "Kazakhstan", "Kenya", "Kiribati", "Korea, Democratic People's Republic of", "Korea, Republic of", "Kosovo", "Kuwait", "Kyrgyzstan", "Lao People's Democratic Republic", "Latvia", "Lebanon", "Lesotho", "Liberia", "Libyan Arab Jamahiriya", "Liechtenstein", "Lithuania", "Luxembourg", "Macau", "Macedonia", "Madagascar", "Malawi", "Malaysia", "Maldives", "Mali", "Malta", "Marshall Islands", "Martinique", "Mauritania", "Mauritius", "Mayotte", "Mexico", "Micronesia, Federated States of", "Moldova, Republic of", "Monaco", "Mongolia", "Montserrat", "Morocco", "Mozambique", "Myanmar", "Namibia", "Nauru", "Nepal", "Netherlands", "Netherlands Antilles", "New Caledonia", "New Zealand", "Nicaragua", "Niger", "Nigeria", "Niue", "Norfork Island", "Northern Mariana Islands", "Norway", "Oman", "Pakistan", "Palau", "Panama", "Papua New Guinea", "Paraguay", "Peru", "Philippines", "Pitcairn", "Poland", "Portugal", "Puerto Rico", "Qatar", "Reunion", "Romania", "Russian Federation", "Rwanda", "Saint Kitts and Nevis", "Saint Lucia", "Saint Vincent and the Grenadines", "Samoa", "San Marino", "Sao Tome and Principe", "Saudi Arabia", "Senegal", "Seychelles", "Sierra Leone", "Singapore", "Slovakia", "Slovenia", "Solomon Islands", "Somalia", "South Africa", "South Georgia South Sandwich Islands", "South Sudan", "Spain", "Sri Lanka", "St. Helena", "St. Pierre and Miquelon", "Sudan", "Suriname", "Svalbarn and Jan Mayen Islands", "Swaziland", "Sweden", "Switzerland", "Syrian Arab Republic", "Taiwan", "Tajikistan", "Tanzania, United Republic of", "Thailand", "Togo", "Tokelau", "Tonga", "Trinidad and Tobago", "Tunisia", "Turkey", "Turkmenistan", "Turks and Caicos Islands", "Tuvalu", "Uganda", "Ukraine", "United Arab Emirates", "United Kingdom", "United States minor outlying islands", "Uruguay", "Uzbekistan", "Vanuatu", "Vatican City State", "Venezuela", "Vietnam", "Virigan Islands (British)", "Virgin Islands (U.S.)", "Wallis and Futuna Islands", "Western Sahara", "Yemen", "Yugoslavia", "Zaire", "Zambia", "Zimbabwe"
            };

            foreach (var country in countries)
            {
                data.Countries.Add(new Country { Name = country });
            }

            data.SaveChanges();
        }
    }
}
