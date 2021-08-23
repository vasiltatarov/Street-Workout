namespace StreetWorkout.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using StreetWorkout.Data.Models;

    public class UserWorkoutPaymentConfiguration : IEntityTypeConfiguration<UserWorkoutPayment>
    {
        public void Configure(EntityTypeBuilder<UserWorkoutPayment> payment)
        {
            payment
                .HasOne(x => x.User)
                .WithMany(x => x.UserWorkoutPayments)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            payment
                .HasOne(x => x.GroupWorkout)
                .WithMany(x => x.UserWorkoutPayments)
                .HasForeignKey(x => x.GroupWorkoutId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
