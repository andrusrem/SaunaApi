namespace SaunaApi.Models
{
    public class Order
    {
        public int Id { get; set;} 
        public int User_id { get; set;}
        public List<BookedTime> ListTime {get; set;}
        public decimal Price { get; set;}
        public bool Is_it_payd { get; set;}
        public DateTime Created_at { get; set;} = DateTime.Now;
    }
}