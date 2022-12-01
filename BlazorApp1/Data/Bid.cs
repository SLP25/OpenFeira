namespace BlazorApp1.Data
{
    public class Bid
    {
        public int id { get; set; }
        public Product product { get; set; }
        public Stand stand { get; set; }
        public Buyer buyer { get; set; }
        public decimal price { get; set; }
        public DateTime timestamp { get; set; }
        public int amount { get; set; }
    }
}
