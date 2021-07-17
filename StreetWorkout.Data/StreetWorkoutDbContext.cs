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
    }
}
