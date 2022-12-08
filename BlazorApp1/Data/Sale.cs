namespace BlazorApp1.Data
{
    public class Sale
    {
        public int Id { get; set; }
        public Seller Seller { get; set; }
        public DateTime Timestamp { get; set; }
        public Bid Bid { get; set; }
    }
}
