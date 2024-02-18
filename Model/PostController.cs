using Microsoft.AspNetCore.Mvc;

namespace backendSocial.Model
{
    [Route("api/[controller]")]
    public class PostController : ControllerBase
    {
        private readonly PostService _postService;

        public PostController(PostService postService)
        {
            _postService = postService;
        }

        [HttpPost]
        public IActionResult CreatePost([FromBody] Post post)
        {
            try
            {
                _postService.CreatePost(post);
                return CreatedAtRoute("GetPost", new { id = post.Id }, post);
            }
            catch (ValidationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (DataAccessException ex)
            {
                return StatusCode(500, new { message = "Internal server error." });
            }
        }

        [HttpGet]
        public IActionResult GetAllPosts()
        {
            try
            {
                var posts = _postService.ReadAllPosts();
                return Ok(posts);
            }
            catch (DataAccessException ex)
            {
                return StatusCode(500, new { message = "Internal server error." });
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetPostById(int id)
        {
            try
            {
                var post = _postService.ReadPostById(id);
                if (post == null)
                {
                    return NotFound(new { message = $"Post with ID {id} not found." });
                }
                return Ok(post);
            }
            catch (DataAccessException ex)
            {
                return StatusCode(500, new { message = "Internal server error." });
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdatePost(int id, [FromBody] Post post)
        {
            if (id != post.Id)
            {
                return BadRequest(new { message = "Post ID in URL and body must match." });
            }

            try
            {
                _postService.UpdatePost(post);
                return NoContent();
            }
            catch (ValidationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (DataAccessException ex)
            {
                return StatusCode(500, new { message = "Internal server error." });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePost(int id)
        {
            try
            {
                _postService.DeletePost(id);
                return NoContent();
            }
            catch (DataAccessException ex)
            {
                return StatusCode(500, new { message = "Internal server error." });
            }
        }
    }
}