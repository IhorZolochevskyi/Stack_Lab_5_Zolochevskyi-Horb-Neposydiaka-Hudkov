namespace lab5.Models
{
    public class Car
    {
        public int Id { get; set; }
        public string brand {  get; set; }
        public string model { get; set; }
        public string carNumber { get; set; }
        public decimal pricePerDay {  get; set; }
        public bool isRented { get; set; }
    }
}
