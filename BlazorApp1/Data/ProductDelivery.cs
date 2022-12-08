namespace BlazorApp1.Data
{
    public class ProductDelivery
    {
        public int Id { get; set; }
        public Product Product { get; set; }

        public int Amount { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
