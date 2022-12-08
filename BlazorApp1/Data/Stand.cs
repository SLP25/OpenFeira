namespace BlazorApp1.Data
{
    public class Stand
    {
        public int Id { get; set; }
        public Seller Seller { get; set; }

        public Market Market { get; set; }
        public string PhotoPath { get; set; }

        List<Product> Products { get; set; }

    }
}
