namespace StreetWorkout.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    using Models;

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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<ApplicationUser>()
                .HasOne(x => x.Country)
                .WithMany(x => x.Users)
                .HasForeignKey(x => x.CountryId)
                .OnDelete(DeleteBehavior.Restrict);

            //builder
            //    .Entity<UserData>()
            //    .HasOne<ApplicationUser>()
            //    .WithOne()
            //    .HasForeignKey<UserData>(x => x.UserId)
            //    .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<UserData>()
                .HasOne(x => x.Sport)
                .WithMany()
                .HasForeignKey(x => x.SportId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<UserData>()
                .HasOne(x => x.Goal)
                .WithMany()
                .HasForeignKey(x => x.GoalId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<UserData>()
                .HasOne(x => x.TrainingFrequency)
                .WithMany()
                .HasForeignKey(x => x.TrainingFrequencyId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }
    }
}
