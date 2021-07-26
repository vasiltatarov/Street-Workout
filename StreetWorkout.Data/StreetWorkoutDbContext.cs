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

        public DbSet<BodyPart> BodyParts { get; set; }

        public DbSet<Workout> Workouts { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Vote> Votes { get; set; }

        public DbSet<GroupWorkout> GroupWorkouts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<ApplicationUser>()
                .HasOne(x => x.Country)
                .WithMany(x => x.Users)
                .HasForeignKey(x => x.CountryId)
                .OnDelete(DeleteBehavior.Restrict);

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

            builder
                .Entity<Workout>()
                .HasOne(x => x.User)
                .WithMany(x => x.Workouts)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Workout>()
                .HasOne(x => x.Sport)
                .WithMany(x => x.Workouts)
                .HasForeignKey(x => x.SportId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Workout>()
                .HasOne(x => x.BodyPart)
                .WithMany(x => x.Workouts)
                .HasForeignKey(x => x.BodyPartId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Vote>()
                .HasOne(x => x.User)
                .WithMany(x => x.Votes)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<GroupWorkout>()
                .HasOne(x => x.Sport)
                .WithMany(x => x.GroupWorkouts)
                .HasForeignKey(x => x.SportId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<GroupWorkout>()
                .HasOne(x => x.Trainer)
                .WithMany(x => x.GroupWorkouts)
                .HasForeignKey(x => x.TrainerId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }
    }
}
