namespace BlazorApp1.Data
{
    public class Market
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public int NumberOfStands { get; set; }
        public DateTime StartingTime { get; set; }
        public DateTime EndingTime { get; set; }
        public Organizer Organizer { get; set; }
        public string PhotoPath { get; set; }
    }
}
