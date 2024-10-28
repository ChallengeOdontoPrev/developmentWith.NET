using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OdontoPrev.Services;
using OdontoPrev.ViewModels;
using System.Security.Claims;

namespace OdontoPrev.Pages.Author
{
    [Authorize]
    public class DashboardModel : PageModel
    {
        private readonly IBlogService _blogService;
        private readonly IAuthorService _authorService;

        public DashboardModel(IBlogService blogService, IAuthorService authorService)
        {
            _blogService = blogService;
            _authorService = authorService;
            CurrentAuthor = new AuthorViewModel();
            AuthorPosts = new List<PostViewModel>();
            NewPost = new PostViewModel();
        }

        public AuthorViewModel CurrentAuthor { get; set; }
        public IEnumerable<PostViewModel> AuthorPosts { get; set; }

        [BindProperty]
        public PostViewModel NewPost { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                var authorId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
                var author = await _authorService.GetByIdAsync(authorId);

                if (author == null)
                {
                    return RedirectToPage("/Login");
                }

                CurrentAuthor = author;
                AuthorPosts = await _blogService.GetPostsByAuthorIdAsync(authorId);

                return Page();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Ocorreu um erro ao carregar o dashboard.";
                return RedirectToPage("/Login");
            }
        }

        public async Task<IActionResult> OnPostCreatePostAsync()
        {
            try
            {
                var authorId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
                if (authorId == 0)
                {
                    return RedirectToPage("/Login");
                }

                NewPost.AuthorId = authorId;
                NewPost.CreatedAt = DateTime.UtcNow;

                await _blogService.CreatePostAsync(NewPost);
                TempData["Message"] = "Post criado com sucesso!";
                return RedirectToPage();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Erro ao criar o post. Tente novamente.";
                await LoadPageData();
                return Page();
            }
        }

        public async Task<IActionResult> OnPostUpdatePostAsync(PostViewModel post)
        {
            try
            {
                var authorId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
                var existingPost = await _blogService.GetPostByIdAsync(post.Id);

                if (existingPost == null)
                {
                    TempData["ErrorMessage"] = "Post não encontrado.";
                    return RedirectToPage();
                }

                if (existingPost.AuthorId != authorId)
                {
                    TempData["ErrorMessage"] = "Você não tem permissão para editar este post.";
                    return RedirectToPage();
                }

                // Mantém os dados originais que não devem ser alterados
                post.AuthorId = existingPost.AuthorId;
                post.CreatedAt = existingPost.CreatedAt;
                post.AuthorName = existingPost.AuthorName;

                await _blogService.UpdatePostAsync(post);
                TempData["Message"] = "Post atualizado com sucesso!";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Erro ao atualizar o post. Tente novamente.";
                await LoadPageData();
            }

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeletePostAsync(int id)
        {
            try
            {
                var authorId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
                var post = await _blogService.GetPostByIdAsync(id);

                if (post == null)
                {
                    TempData["ErrorMessage"] = "Post não encontrado.";
                    return RedirectToPage();
                }

                if (post.AuthorId != authorId)
                {
                    TempData["ErrorMessage"] = "Você não tem permissão para excluir este post.";
                    return RedirectToPage();
                }

                await _blogService.DeletePostAsync(id);
                TempData["Message"] = "Post excluído com sucesso!";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Erro ao excluir o post. Tente novamente.";
            }

            return RedirectToPage();
        }

        private async Task LoadPageData()
        {
            try
            {
                var authorId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
                CurrentAuthor = await _authorService.GetByIdAsync(authorId);
                AuthorPosts = await _blogService.GetPostsByAuthorIdAsync(authorId);
            }
            catch
            {
                CurrentAuthor = new AuthorViewModel();
                AuthorPosts = new List<PostViewModel>();
            }
        }
    }
}