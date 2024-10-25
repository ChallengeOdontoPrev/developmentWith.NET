using Microsoft.AspNetCore.Mvc.RazorPages;
using OdontoPrev.Services;
using OdontoPrev.ViewModels;

public class IndexModel : PageModel
{
    private readonly IBlogService _blogService;

    public IndexModel(IBlogService blogService)
    {
        _blogService = blogService;
    }

    public IEnumerable<PostViewModel> Posts { get; set; }

    public async Task OnGetAsync()
    {
        Posts = await _blogService.GetAllPostsAsync();
    }
}