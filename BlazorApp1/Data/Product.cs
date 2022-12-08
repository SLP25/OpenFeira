namespace BlazorApp1.Data
{
    public class Product
    {
        public int id { get; set; }
        public string name { get; set; }

        public string description { get; set; }
        public decimal basePrice { get; set; }
        public int stock { get; set; }

        List<string> photos { get; set; }

        public static string testVar = "NAO";

    }
}
