namespace StreetWorkout.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using StreetWorkout.Data.Models;

    public class UserDataConfiguration : IEntityTypeConfiguration<UserData>
    {
        public void Configure(EntityTypeBuilder<UserData> userData)
        {
            userData
                .HasOne(x => x.Sport)
                .WithMany()
                .HasForeignKey(x => x.SportId)
                .OnDelete(DeleteBehavior.Restrict);

            userData
                .HasOne(x => x.Goal)
                .WithMany()
                .HasForeignKey(x => x.GoalId)
                .OnDelete(DeleteBehavior.Restrict);

            userData
                .HasOne(x => x.TrainingFrequency)
                .WithMany()
                .HasForeignKey(x => x.TrainingFrequencyId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
