namespace lab5.Models
{
    public class Document
    {
        public int Id { get; set; }
        public Client client { get; set; }
        public Car car { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
    }
}
