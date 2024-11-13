    namespace lab5.Models
    {
        public class Planting
        {
            public int Id { get; set; }
            public string CultureType { get; set; }
            public string Name { get; set; }
            public int AverageLifetime { get; set; }
            public int Quantity {  get; set; }

            public int ParkId { get; set; }
            public Park Park { get; set; }
        }
    }
