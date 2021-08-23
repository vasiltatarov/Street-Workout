namespace StreetWorkout
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using StreetWorkout.Data;
    using StreetWorkout.Data.Models;
    using StreetWorkout.Hubs;
    using StreetWorkout.Infrastructure;
    using StreetWorkout.Services.Accounts;
    using StreetWorkout.Services.BodyCalculators;
    using StreetWorkout.Services.Chat;
    using StreetWorkout.Services.Comments;
    using StreetWorkout.Services.GroupWorkouts;
    using StreetWorkout.Services.Homes;
    using StreetWorkout.Services.Statistics;
    using StreetWorkout.Services.SupplementCategories;
    using StreetWorkout.Services.Supplements;
    using StreetWorkout.Services.Trainings;
    using StreetWorkout.Services.Votes;
    using StreetWorkout.Services.WorkoutPayments;
    using StreetWorkout.Services.Workouts;

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
            services.AddSignalR();

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
            services.AddTransient<IChatService, ChatService>();
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
                    endpoints.MapHub<ChatHub>("/chat");
                    endpoints.MapDefaultAreaRoute();
                    endpoints.MapDefaultRoutes();
                    endpoints.MapDefaultControllerRoute();
                    endpoints.MapRazorPages();
                });
        }
    }
}
