namespace StreetWorkout.Data.Models
{
    public class SupplementPayment
    {
        public int Id { get; set; }

        public int SupplementId { get; set; }

        public Supplement Supplement { get; set; }

        public int PaymentId { get; set; }

        public Payment Payment { get; set; }
    }
}
