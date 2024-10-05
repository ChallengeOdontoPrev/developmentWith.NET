using OdontoPrev.Models;
using OdontoPrev.Repositories;
using OdontoPrev.ViewModels;
using System.Security.Cryptography;
using System.Text;

namespace OdontoPrev.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorService(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<AuthorViewModel> GetByIdAsync(int id)
        {
            var author = await _authorRepository.GetByIdAsync(id);
            return MapToAuthorViewModel(author);
        }

        public async Task<IEnumerable<AuthorViewModel>> GetAllAsync()
        {
            var authors = await _authorRepository.GetAllAsync();
            return authors.Select(MapToAuthorViewModel);
        }

        public async Task<AuthorViewModel> AuthenticateAsync(string username, string password)
        {
            var author = await _authorRepository.GetByUsernameAsync(username);
            if (author == null)
                return null;

            if (!VerifyPasswordHash(password, author.PasswordHash))
                return null;

            return MapToAuthorViewModel(author);
        }

        private AuthorViewModel MapToAuthorViewModel(Author author)
        {
            return new AuthorViewModel
            {
                Id = author.Id,
                Name = author.Name,
                Email = author.Email,
                Username = author.Username
            };
        }

        private bool VerifyPasswordHash(string password, string storedHash)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                var hash = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
                return hash == storedHash;
            }
        }
    }
}