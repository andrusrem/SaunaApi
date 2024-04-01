namespace SaunaApi.Models
{
    public class BookedTime
    {
        public int Id { get; set;} 
        public int User_id { get; set;}
        //Time format is 0001-01-01T00:00:00
        public DateTime Booked_time { get; set;}
    }
}