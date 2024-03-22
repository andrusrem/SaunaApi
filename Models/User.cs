namespace SaunaApi.Models
{
    public class User
    {
        internal object _context;

        public int Id { get; set;} 
        public string Username { get; set;}
        public string Email { get; set;}
        public string Firstname { get; set;}
        public string Lastname { get; set;}
        public string Password { get; set;}
        public string Access_token { get; set;}
        public DateTime Created_at { get; set;} = DateTime.Now;
    }
}