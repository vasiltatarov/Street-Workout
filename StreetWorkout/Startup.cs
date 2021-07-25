namespace StreetWorkout
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.AspNetCore.Identity;

    using Data;
    using Data.Models;
    using Services.Accounts;
    using Services.Homes;
    using Services.Trainings;
    using Services.Statistics;
    using Infrastructure;
    using Services.Comments;
    using Services.Workouts;
    using Services.Votes;

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

            services
                .AddAntiforgery(options =>
            {
                options.HeaderName = "X-CSRF-TOKEN";
            });

            services
                .AddAuthentication()
                .AddFacebook(options =>
            {
                options.AppId = Configuration["FacebookAuthentication:AppId"];
                options.AppSecret = Configuration["FacebookAuthentication:AppSecret"];
            });

            services.AddControllersWithViews();

            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IHomeService, HomeService>();
            services.AddTransient<ITrainerService, TrainerService>();
            services.AddTransient<IStatisticsService, StatisticsService>();
            services.AddTransient<IWorkoutService, WorkoutService>();
            services.AddTransient<ICommentService, CommentService>();
            services.AddTransient<IVoteService, VoteService>();
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
                    endpoints.MapDefaultControllerRoute();
                    endpoints.MapRazorPages();
                });
        }
    }
}
