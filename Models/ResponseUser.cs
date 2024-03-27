namespace SaunaApi.Models
{
    public class ResponseUser
    {
        public int? Id { get; set;} 
        public string? Username { get; set;}
        public string? Email { get; set;}
        public string? Firstname { get; set;}
        public string? Lastname { get; set;}
        public string? Password { get; set;}
        public string? Access_token { get; set;}
    }
}