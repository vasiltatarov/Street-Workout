namespace StreetWorkout.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    public class SupplementPaymentConfiguration : IEntityTypeConfiguration<SupplementPayment>
    {
        public void Configure(EntityTypeBuilder<SupplementPayment> supplementPayment)
        {
            supplementPayment
                .HasOne(x => x.Payment)
                .WithMany(x => x.SupplementPayments)
                .HasForeignKey(x => x.PaymentId)
                .OnDelete(DeleteBehavior.Restrict);

            supplementPayment
                .HasOne(x => x.Supplement)
                .WithMany(x => x.SupplementPayments)
                .HasForeignKey(x => x.SupplementId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
