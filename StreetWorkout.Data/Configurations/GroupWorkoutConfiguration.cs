namespace StreetWorkout.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using StreetWorkout.Data.Models;

    public class GroupWorkoutConfiguration : IEntityTypeConfiguration<GroupWorkout>
    {
        public void Configure(EntityTypeBuilder<GroupWorkout> groupWorkout)
        {
            groupWorkout
                .HasOne(x => x.Sport)
                .WithMany(x => x.GroupWorkouts)
                .HasForeignKey(x => x.SportId)
                .OnDelete(DeleteBehavior.Restrict);

            groupWorkout
                .HasOne(x => x.Trainer)
                .WithMany(x => x.GroupWorkouts)
                .HasForeignKey(x => x.TrainerId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
