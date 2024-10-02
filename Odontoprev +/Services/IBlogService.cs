using OdontoPrev.ViewModels;

namespace OdontoPrev.Services
{
    public interface IBlogService
    {
        Task<AuthorViewModel> GetAuthorByIdAsync(int id);
        Task<PostViewModel> GetPostByIdAsync(int id);
        Task<IEnumerable<PostViewModel>> GetPostsByAuthorIdAsync(int authorId);
        Task<IEnumerable<PostViewModel>> GetAllPostsAsync();
        Task<PostViewModel> CreatePostAsync(PostViewModel postViewModel);
        Task UpdatePostAsync(PostViewModel postViewModel);
        Task DeletePostAsync(int id);
    }
}