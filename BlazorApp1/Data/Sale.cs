namespace BlazorApp1.Data
{
    public class Sale
    {
        public int id { get; set; }
        public Seller seller { get; set; }
        public DateTime timestamp { get; set; }
        public Bid bid { get; set; }
    }
}
