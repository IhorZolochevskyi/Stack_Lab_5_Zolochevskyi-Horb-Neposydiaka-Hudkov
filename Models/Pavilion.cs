namespace lab5.Models
{
    public class Pavilion
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public decimal Square { get; set; }

        public int ParkId { get; set; }
        public Park Park { get; set; }
    }
}
