using Microsoft.AspNetCore.Mvc;
using OdontoPrev.Services;
using OdontoPrev.ViewModels;

namespace OdontoPrev.Controllers
{
    public class PostController : Controller
    {
        private readonly IBlogService _blogService;

        public PostController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        public async Task<IActionResult> Index()
        {
            var posts = await _blogService.GetAllPostsAsync();
            return View(posts);
        }

        public async Task<IActionResult> Details(int id)
        {
            var post = await _blogService.GetPostByIdAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            return View(post);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PostViewModel postViewModel)
        {
            if (ModelState.IsValid)
            {
                await _blogService.CreatePostAsync(postViewModel);
                return RedirectToAction(nameof(Index));
            }
            return View(postViewModel);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var post = await _blogService.GetPostByIdAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            return View(post);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PostViewModel postViewModel)
        {
            if (id != postViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _blogService.UpdatePostAsync(postViewModel);
                return RedirectToAction(nameof(Index));
            }
            return View(postViewModel);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var post = await _blogService.GetPostByIdAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            return View(post);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _blogService.DeletePostAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}