using OdontoPrev.Models;

namespace OdontoPrev.Repositories
{
    public interface IAuthorRepository
    {
        Task<Author> GetByIdAsync(int id);
        Task<IEnumerable<Author>> GetAllAsync();
        Task<Author> AddAsync(Author author);
        Task UpdateAsync(Author author);
        Task DeleteAsync(int id);
    }
}