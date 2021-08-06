namespace StreetWorkout.Infrastructure
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    using StreetWorkout.Data;
    using StreetWorkout.Data.Models;
    using StreetWorkout.Data.Models.Enums;
    using StreetWorkout.Infrastructure.Seeder;

    using static WebConstants;

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder PrepareDatabase(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            var services = serviceScope.ServiceProvider;

            MigrateDatabase(services);

            SeedCountries(services);
            SeedSports(services);
            SeedGoals(services);
            SeedTrainingFrequencies(services);
            SeedBodyParts(services);

            SeedAdministrator(services);
            CompleteUserAdministratorAccount(services);

            SeedWorkouts(services);

            SeedSupplementCategories(services);
            SeedSupplements(services);

            return app;
        }

        private static void SeedSupplements(IServiceProvider services)
        {
            var data = services.GetRequiredService<StreetWorkoutDbContext>();

            if (data.Supplements.Any())
            {
                return;
            }

            data.Supplements.AddRange(SupplementsSeeder.GetSupplements());
            data.SaveChanges();
        }

        private static void SeedSupplementCategories(IServiceProvider services)
        {
            var data = services.GetRequiredService<StreetWorkoutDbContext>();

            if (data.SupplementCategories.Any())
            {
                return;
            }

            data.SupplementCategories.AddRange(new SupplementCategory[]
            {
                new () { Name = "Pre Workout" },
                new () { Name = "Post Workout" },
                new () { Name = "Intra Workout" },
                new () { Name = "Amino" },
                new () { Name = "Creatine" },
                new () { Name = "Protein" },
                new () { Name = "Gainer" },
                new () { Name = "Vitamins" },
                new () { Name = "Hormone-Stimulating" },
            });
            data.SaveChanges();
        }

        private static void CompleteUserAdministratorAccount(IServiceProvider services)
        {
            var data = services.GetRequiredService<StreetWorkoutDbContext>();
            var user = data.Users.FirstOrDefault(x => x.UserName == "Vasilkovski");

            if (user == null || user.IsAccountCompleted)
            {
                return;
            }

            var userData = new UserData
            {
                UserId = user.Id,
                SportId = 12,
                GoalId = 6,
                TrainingFrequencyId = 2,
                Weight = 80,
                Height = 180,
                Description = "My passion is street workouts.",
            };

            user.IsAccountCompleted = true;
            data.UserDatas.Add(userData);
            data.SaveChanges();
        }

        private static void SeedAdministrator(IServiceProvider services)
        {
            var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            Task
                .Run(async () =>
                {
                    if (await roleManager.RoleExistsAsync(AdministratorRoleName))
                    {
                        return;
                    }

                    var role = new IdentityRole { Name = AdministratorRoleName };
                    await roleManager.CreateAsync(role);

                    var user = new ApplicationUser
                    {
                        Email = "Vasko@gmail.com",
                        UserName = "Vasilkovski",
                        ImageUrl = "https://d3t3ozftmdmh3i.cloudfront.net/production/podcast_uploaded_nologo400/2861978/2861978-1583463193883-7b2af23b7b533.jpg",
                        CountryId = 126,
                        City = "Chakalarovo",
                        UserRole = UserRole.Trainer,
                        Gender = Gender.Male,
                        DateOfBirth = DateTime.UtcNow,
                    };

                    await userManager.CreateAsync(user, "vasko123");
                    await userManager.AddToRoleAsync(user, AdministratorRoleName);
                })
                .GetAwaiter()
                .GetResult();
        }

        private static void MigrateDatabase(IServiceProvider services)
        {
            var data = services.GetRequiredService<StreetWorkoutDbContext>();
            data.Database.Migrate();
        }

        private static void SeedWorkouts(IServiceProvider services)
        {
            var data = services.GetRequiredService<StreetWorkoutDbContext>();

            if (data.Workouts.Any())
            {
                return;
            }

            var rand = new Random();
            var userId = data.UserRoles.First().UserId;

            for (int i = 1; i < 40; i++)
            {
                data.Workouts.Add(new Workout
                {
                    Title = "Test" + i,
                    SportId = rand.Next(1, 13),
                    DifficultLevel = (DifficultLevel)rand.Next(1, 4),
                    BodyPartId = rand.Next(1, 14),
                    UserId = userId,
                    Minutes = rand.Next(20, 130),
                    Content = "Test" + i,
                    CreatedOn = DateTime.UtcNow,
                });
            }

            data.SaveChanges();
        }

        private static void SeedBodyParts(IServiceProvider services)
        {
            var data = services.GetRequiredService<StreetWorkoutDbContext>();
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

        private static void SeedTrainingFrequencies(IServiceProvider services)
        {
            var data = services.GetRequiredService<StreetWorkoutDbContext>();

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

        private static void SeedGoals(IServiceProvider services)
        {
            var data = services.GetRequiredService<StreetWorkoutDbContext>();

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

        private static void SeedSports(IServiceProvider services)
        {
            var data = services.GetRequiredService<StreetWorkoutDbContext>();

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

        private static void SeedCountries(IServiceProvider services)
        {
            var data = services.GetRequiredService<StreetWorkoutDbContext>();

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
