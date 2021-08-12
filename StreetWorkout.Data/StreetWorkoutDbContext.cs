namespace StreetWorkout.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    using Models;
    using Configurations;

    public class StreetWorkoutDbContext : IdentityDbContext<ApplicationUser>
    {
        public StreetWorkoutDbContext(DbContextOptions<StreetWorkoutDbContext> options)
            : base(options)
        {
        }

        public DbSet<Country> Countries { get; set; }

        public DbSet<Sport> Sports { get; set; }

        public DbSet<Goal> Goals { get; set; }

        public DbSet<TrainingFrequency> TrainingFrequencies { get; set; }

        public DbSet<UserData> UserDatas { get; set; }

        public DbSet<BodyPart> BodyParts { get; set; }

        public DbSet<Workout> Workouts { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Vote> Votes { get; set; }

        public DbSet<GroupWorkout> GroupWorkouts { get; set; }

        public DbSet<UserWorkoutPayment> UserWorkoutPayments { get; set; }

        public DbSet<Supplement> Supplements { get; set; }

        public DbSet<SupplementCategory> SupplementCategories { get; set; }

        public DbSet<ChatMessage> ChatMessages { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ApplicationUserConfiguration());
            builder.ApplyConfiguration(new UserDataConfiguration());
            builder.ApplyConfiguration(new WorkoutConfiguration());
            builder.ApplyConfiguration(new VoteConfiguration());
            builder.ApplyConfiguration(new GroupWorkoutConfiguration());
            builder.ApplyConfiguration(new UserWorkoutPaymentConfiguration());
            builder.ApplyConfiguration(new SupplementConfiguration());
            builder.ApplyConfiguration(new ChatMessageConfiguration());

            base.OnModelCreating(builder);
        }
    }
}
