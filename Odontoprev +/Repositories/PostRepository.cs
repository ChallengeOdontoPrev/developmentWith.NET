using Microsoft.EntityFrameworkCore;
using OdontoPrev.Models;
using OdontoPrev.Data;

namespace OdontoPrev.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly BlogDbContext _context;

        public PostRepository(BlogDbContext context)
        {
            _context = context;
        }

        public async Task<Post> GetByIdAsync(int id)
        {
            return await _context.Posts
                .Include(p => p.Author)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Post>> GetAllAsync()
        {
            return await _context.Posts
                .Include(p => p.Author)
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<Post>> GetByAuthorIdAsync(int authorId)
        {
            return await _context.Posts
                .Where(p => p.AuthorId == authorId)
                .Include(p => p.Author)
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();
        }

        public async Task<Post> AddAsync(Post post)
        {
            try
            {
                _context.Posts.Add(post);
                await _context.SaveChangesAsync();

                // Recarrega o post com as informações do autor
                await _context.Entry(post)
                    .Reference(p => p.Author)
                    .LoadAsync();

                return post;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Erro ao criar o post.", ex);
            }
        }

        public async Task UpdateAsync(Post post)
        {
            try
            {
                var existingPost = await _context.Posts
                    .Include(p => p.Author)
                    .FirstOrDefaultAsync(p => p.Id == post.Id);

                if (existingPost == null)
                {
                    throw new InvalidOperationException("Post não encontrado.");
                }

                // Atualiza apenas os campos permitidos
                existingPost.Title = post.Title;
                existingPost.Content = post.Content;
                // Não atualiza CreatedAt, AuthorId e Author

                _context.Entry(existingPost).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await PostExists(post.Id))
                {
                    throw new InvalidOperationException("Post não encontrado.");
                }
                throw;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Erro ao atualizar o post.", ex);
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                var post = await _context.Posts.FindAsync(id);
                if (post == null)
                {
                    throw new InvalidOperationException("Post não encontrado.");
                }

                _context.Posts.Remove(post);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Erro ao excluir o post.", ex);
            }
        }

        private async Task<bool> PostExists(int id)
        {
            return await _context.Posts.AnyAsync(p => p.Id == id);
        }
    }
}