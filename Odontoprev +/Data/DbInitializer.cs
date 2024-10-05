using OdontoPrev.Models;
using System.Security.Cryptography;
using System.Text;

namespace OdontoPrev.Data
{
    public static class DbInitializer
    {
        public static void Initialize(BlogDbContext context)
        {
            context.Database.EnsureCreated();

            // Check if there are any authors
            if (context.Authors.Any())
            {
                return;  
            }

            var authors = new Author[]
            {
                new Author { Name = "João Silva", Email = "joao@example.com", Username = "joaosilva", PasswordHash = HashPassword("senha123") },
                new Author { Name = "Maria Santos", Email = "maria@example.com", Username = "mariasantos", PasswordHash = HashPassword("senha456") },
                new Author { Name = "Pedro Oliveira", Email = "pedro@example.com", Username = "pedrooliveira", PasswordHash = HashPassword("senha789") },
                new Author { Name = "Ana Rodrigues", Email = "ana@example.com", Username = "anarodrigues", PasswordHash = HashPassword("senha321") },
                new Author { Name = "Carlos Ferreira", Email = "carlos@example.com", Username = "carlosferreira", PasswordHash = HashPassword("senha654") }
            };

            foreach (Author a in authors)
            {
                context.Authors.Add(a);
            }
            context.SaveChanges();
        }

        private static string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }
    }
}