namespace SaunaApi.Models
{
    public class BookedTime
    {
        public int Id { get; set;} 
        public int User_id { get; set;}
        public User user { get; set;}
        public DateTime Booked_time { get; set;}
    }
}