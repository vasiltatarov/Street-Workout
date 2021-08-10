namespace StreetWorkout
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    using Data;
    using Data.Models;
    using Infrastructure;
    using Services.Accounts;
    using Services.BodyCalculators;
    using Services.Comments;
    using Services.GroupWorkouts;
    using Services.Homes;
    using Services.Trainings;
    using Services.Statistics;
    using Services.Votes;
    using Services.Supplements;
    using Services.SupplementCategories;
    using Services.Workouts;
    using Services.WorkoutPayments;

    public class Startup
    {
        public Startup(IConfiguration configuration)
            => Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<StreetWorkoutDbContext>(options => options
                .UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddDatabaseDeveloperPageExceptionFilter();

            services
                .AddDefaultIdentity<ApplicationUser>(options =>
                {
                    options.Password.RequireLowercase = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequiredLength = 6;
                })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<StreetWorkoutDbContext>();

            services.AddAutoMapper(typeof(Startup));

            services
                .AddAuthentication()
                .AddFacebook(options =>
            {
                options.AppId = WebKeys.FacebookAuthenticationAppId;
                options.AppSecret = WebKeys.FacebookAuthenticationAppSecret;
            });

            services
                .AddAntiforgery(options =>
                {
                    options.HeaderName = "X-CSRF-TOKEN";
                });

            services.AddControllersWithViews(options =>
            {
                options.Filters.Add<AutoValidateAntiforgeryTokenAttribute>();
            });

            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IHomeService, HomeService>();
            services.AddTransient<ITrainerService, TrainerService>();
            services.AddTransient<IStatisticsService, StatisticsService>();
            services.AddTransient<IWorkoutService, WorkoutService>();
            services.AddTransient<ICommentService, CommentService>();
            services.AddTransient<IVoteService, VoteService>();
            services.AddTransient<IGroupWorkoutService, GroupWorkoutService>();
            services.AddTransient<IBodyCalculatorService, BodyCalculatorService>();
            services.AddTransient<ISupplementService, SupplementService>();
            services.AddTransient<ISupplementCategoryService, SupplementCategoryService>();
            services.AddTransient<IWorkoutPaymentService, WorkoutPaymentService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.PrepareDatabase();

            if (env.IsDevelopment())
            {
                app
                    .UseDeveloperExceptionPage()
                    .UseMigrationsEndPoint();
            }
            else
            {
                app
                    .UseExceptionHandler("/Home/Error")
                    .UseHsts();
            }

            app
                .UseHttpsRedirection()
                .UseStaticFiles()
                .UseRouting()
                .UseAuthentication()
                .UseAuthorization()
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapDefaultAreaRoute();
                    endpoints.MapDefaultRoutes();
                    endpoints.MapDefaultControllerRoute();
                    endpoints.MapRazorPages();
                });
        }
    }
}
