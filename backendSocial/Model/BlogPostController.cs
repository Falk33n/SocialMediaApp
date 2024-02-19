using Microsoft.AspNetCore.Mvc;

namespace backendSocial.Model
{
    [Route("api/[controller]")]
    public class BlogPostController : ControllerBase
    {
        private readonly BlogPostService _blogPostService;

        public BlogPostController(BlogPostService blogPostService)
        {
            _blogPostService = blogPostService;
        }

        [HttpPost]
        public IActionResult CreatePost([FromBody] BlogPost blogPost)
        {
            try
            {
                _blogPostService.CreatePost(blogPost);
                return CreatedAtRoute("GetPost", new { id = blogPost.Id }, blogPost);
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
                var posts = _blogPostService.ReadAllPosts();
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
                var post = _blogPostService.ReadPostById(id);
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
        public IActionResult UpdatePost(int id, [FromBody] BlogPost blogPost)
        {
            if (id != blogPost.Id)
            {
                return BadRequest(new { message = "Post ID in URL and body must match." });
            }

            try
            {
                _blogPostService.UpdatePost(blogPost);
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
                _blogPostService.DeletePost(id);
                return NoContent();
            }
            catch (DataAccessException ex)
            {
                return StatusCode(500, new { message = "Internal server error." });
            }
        }
    }
}