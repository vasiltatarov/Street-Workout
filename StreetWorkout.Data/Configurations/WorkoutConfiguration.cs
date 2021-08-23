namespace StreetWorkout.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using StreetWorkout.Data.Models;

    public class WorkoutConfiguration : IEntityTypeConfiguration<Workout>
    {
        public void Configure(EntityTypeBuilder<Workout> workout)
        {
            workout
                .HasOne(x => x.User)
                .WithMany(x => x.Workouts)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            workout
                .HasOne(x => x.Sport)
                .WithMany(x => x.Workouts)
                .HasForeignKey(x => x.SportId)
                .OnDelete(DeleteBehavior.Restrict);

            workout
                .HasOne(x => x.BodyPart)
                .WithMany(x => x.Workouts)
                .HasForeignKey(x => x.BodyPartId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
