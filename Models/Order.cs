namespace SaunaApi.Models
{
    public class Order
    {
        public int Id { get; set;} 
        public int User_id { get; set;}
        public User User{ get; set;}
        public int Booked_time_id { get; set;}
        public BookedTime Booked_time{ get; set;}
        public decimal Price { get; set;}
        public bool Is_it_payd { get; set;}
        public DateTime Created_at { get; set;} = DateTime.Now;
    }
}