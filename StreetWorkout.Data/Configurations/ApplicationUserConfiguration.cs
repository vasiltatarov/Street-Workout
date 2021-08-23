namespace StreetWorkout.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using StreetWorkout.Data.Models;

    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> user)
        {
            user
                .HasOne(x => x.Country)
                .WithMany(x => x.Users)
                .HasForeignKey(x => x.CountryId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
