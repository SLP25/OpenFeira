namespace BlazorApp1.Data
{
    public class ProductDelivery
    {
        public int id { get; set; }
        public Product product { get; set; }

        public int amount { get; set; }
        public DateTime timestamp { get; set; }
    }
}
