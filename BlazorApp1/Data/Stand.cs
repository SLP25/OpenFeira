namespace BlazorApp1.Data
{
    public class Stand
    {
        public int id { get; set; }
        public Seller seller { get; set; }

        public Market market { get; set; }
        public Stand stand { get; set; }
        public string photoPath { get; set; }

        List<Product> products { get; set; }

    }
}
