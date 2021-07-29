namespace StreetWorkout.ViewModels.BodyCalculators
{
    using System.ComponentModel.DataAnnotations;

    public class CalculatorFormModel
    {
        [Range(30, 250)]
        public byte Weight { get; set; }

        [Range(50, 250)]
        public byte Height { get; set; }

        public double Activity { get; set; }

        [Range(10, 100)]
        public byte Age { get; set; }

        public int Gender { get; set; }
    }
}
