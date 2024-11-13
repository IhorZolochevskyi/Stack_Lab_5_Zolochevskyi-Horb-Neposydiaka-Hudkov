namespace lab5.Models
{
    public class Park
    {
        public int Id { get; set; }
        public string Name {  get; set; }
        public decimal Square { get; set; }
        public string Location { get; set; }

        public List<Planting> Plantings { get; set; } = new List<Planting>();
        public List<Fountain> Fountains { get; set; } = new List<Fountain>();
        public List<Pavilion> Pavilions { get; set; } = new List<Pavilion>();

    }
}
