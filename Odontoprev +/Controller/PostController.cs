using Microsoft.AspNetCore.Mvc;
using OdontoPrev.Services;
using OdontoPrev.ViewModels;

namespace OdontoPrev.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostController : ControllerBase
    {
        private readonly IBlogService _blogService;

        public PostController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostViewModel>>> GetPosts()
        {
            var posts = await _blogService.GetAllPostsAsync();
            return Ok(posts);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PostViewModel>> GetPost(int id)
        {
            var post = await _blogService.GetPostByIdAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            return Ok(post);
        }

        [HttpPost]
        public async Task<ActionResult<PostViewModel>> CreatePost(PostViewModel postViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdPost = await _blogService.CreatePostAsync(postViewModel);
            return CreatedAtAction(nameof(GetPost), new { id = createdPost.Id }, createdPost);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePost(int id, PostViewModel postViewModel)
        {
            if (id != postViewModel.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _blogService.UpdatePostAsync(postViewModel);
            }
            catch
            {
                if (!await PostExists(id))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            var post = await _blogService.GetPostByIdAsync(id);
            if (post == null)
            {
                return NotFound();
            }

            await _blogService.DeletePostAsync(id);
            return NoContent();
        }

        private async Task<bool> PostExists(int id)
        {
            var post = await _blogService.GetPostByIdAsync(id);
            return post != null;
        }
    }
}