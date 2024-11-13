namespace lab5.Models
{
    public class Fountain
    {
        public int Id { get; set; }
        public int Code { get; set; }
        public DateTime BuildDate { get; set; }
        public decimal MaxWaterConsumption { get; set; }
        public decimal NormalWaterConsumption { get; set; }
        public decimal Square { get; set; }

        public int ParkId { get; set; }
        public Park Park { get; set; }
    }
}
