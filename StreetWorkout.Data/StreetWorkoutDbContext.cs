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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ApplicationUser>()
                .HasOne(x => x.Country)
                .WithMany(x => x.Users)
                .HasForeignKey(x => x.CountryId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }
    }
}
