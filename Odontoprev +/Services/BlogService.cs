using OdontoPrev.Models;
using OdontoPrev.Repositories;
using OdontoPrev.ViewModels;

namespace OdontoPrev.Services
{
    public class BlogService : IBlogService
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IPostRepository _postRepository;

        public BlogService(IAuthorRepository authorRepository, IPostRepository postRepository)
        {
            _authorRepository = authorRepository;
            _postRepository = postRepository;
        }

        public async Task<AuthorViewModel> GetAuthorByIdAsync(int id)
        {
            var author = await _authorRepository.GetByIdAsync(id);
            return MapToAuthorViewModel(author);
        }

        public async Task<PostViewModel> GetPostByIdAsync(int id)
        {
            var post = await _postRepository.GetByIdAsync(id);
            return MapToPostViewModel(post);
        }

        public async Task<IEnumerable<PostViewModel>> GetPostsByAuthorIdAsync(int authorId)
        {
            var posts = await _postRepository.GetByAuthorIdAsync(authorId);
            return posts.Select(MapToPostViewModel);
        }

        public async Task<IEnumerable<PostViewModel>> GetAllPostsAsync()
        {
            var posts = await _postRepository.GetAllAsync();
            return posts.Select(MapToPostViewModel);
        }

        public async Task<PostViewModel> CreatePostAsync(PostViewModel postViewModel)
        {
            var post = MapToPost(postViewModel);
            var createdPost = await _postRepository.AddAsync(post);
            return MapToPostViewModel(createdPost);
        }

        public async Task UpdatePostAsync(PostViewModel postViewModel)
        {
            var post = MapToPost(postViewModel);
            await _postRepository.UpdateAsync(post);
        }

        public async Task DeletePostAsync(int id)
        {
            await _postRepository.DeleteAsync(id);
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

        private PostViewModel MapToPostViewModel(Post post)
        {
            return new PostViewModel
            {
                Id = post.Id,
                Title = post.Title,
                Content = post.Content,
                CreatedAt = post.CreatedAt,
                AuthorId = post.AuthorId,
                AuthorName = post.Author?.Name
            };
        }

        private Post MapToPost(PostViewModel postViewModel)
        {
            return new Post
            {
                Id = postViewModel.Id,
                Title = postViewModel.Title,
                Content = postViewModel.Content,
                CreatedAt = postViewModel.CreatedAt,
                AuthorId = postViewModel.AuthorId
            };
        }
    }
}