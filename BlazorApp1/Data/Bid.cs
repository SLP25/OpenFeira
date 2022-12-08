namespace BlazorApp1.Data
{
    public class Bid
    {
        public int Id { get; set; }
        public Product Product { get; set; }
        public Stand Stand { get; set; }
        public Buyer Buyer { get; set; }
        public decimal Price { get; set; }
        public DateTime Timestamp { get; set; }
        public int Amount { get; set; }
    }
}
