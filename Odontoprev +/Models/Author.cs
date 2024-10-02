using Microsoft.Extensions.Hosting;

namespace OdontoPrev.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public ICollection<Post> Posts { get; set; }
    }
}