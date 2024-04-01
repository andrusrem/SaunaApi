namespace SaunaApi.Models
{
    public class OrderPost
    {
        public int User_id { get; set;}
        public List<BookedTime>? ListTime {get; set;}
        public bool Is_it_payd { get; set;}
    }
}