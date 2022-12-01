namespace BlazorApp1.Data
{
    public class Market
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string location { get; set; }
        public int numberOfStands { get; set; }
        public DateTime startingTime { get; set; }
        public DateTime endingTime { get; set; }
        public Organizer organizer { get; set; }
        public string photoPath { get; set; }
    }
}
