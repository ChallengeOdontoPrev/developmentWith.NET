using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using OdontoPrev.Services;
using OdontoPrev.ViewModels;

public class PostModel : PageModel
{
    private readonly IBlogService _blogService;

    public PostModel(IBlogService blogService)
    {
        _blogService = blogService;
    }

    public PostViewModel Post { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        Post = await _blogService.GetPostByIdAsync(id);

        if (Post == null)
        {
            return NotFound();
        }

        return Page();
    }
}