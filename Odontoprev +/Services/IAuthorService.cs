using OdontoPrev.ViewModels;

namespace OdontoPrev.Services
{
    public interface IAuthorService
    {
        Task<AuthorViewModel> GetByIdAsync(int id);
        Task<IEnumerable<AuthorViewModel>> GetAllAsync();
        Task<AuthorViewModel> AuthenticateAsync(string username, string password);
    }
}