using System.Data.Common;

namespace backendSocial.Model
{
    public class BlogPostService
    {
        private readonly IBlogPostRepository _blogPostRepository;

        public BlogPostService(IBlogPostRepository blogPostRepository)
        {
            _blogPostRepository = blogPostRepository;
        }

        public void CreatePost(BlogPost blogPost)
        {
            if (!ValidationUtils.IsValid(blogPost))
            {
                throw new ValidationException("Title, Author, and Content are required fields.");
            }

            try
            {
                _blogPostRepository.CreatePost(blogPost);
            }
            catch (DbException ex)
            {
                throw new DataAccessException("Failed to create post.", ex);
            }
        }

        public List<BlogPost> ReadAllPosts()
        {
            try
            {
                return _blogPostRepository.ReadAllPosts();
            }
            catch (DbException ex)
            {
                throw new DataAccessException("Failed to retrieve all posts.", ex);
            }
        }

        public BlogPost ReadPostById(int id)
        {
            try
            {
                var post = _blogPostRepository.ReadPostById(id);
                if (post == null)
                {
                    throw new PostNotFoundException($"Post with ID {id} not found.");
                }
                return post;
            }
            catch (DbException ex)
            {
                throw new DataAccessException("Failed to retrieve post.", ex);
            }
        }

        public void UpdatePost(BlogPost blogPost)
        {
            if (!ValidationUtils.IsValid(blogPost))
            {
                throw new ValidationException("Title, Author, and Content are required fields.");
            }

            try
            {
                _blogPostRepository.UpdatePost(blogPost);
            }
            catch (DbException ex)
            {
                throw new DataAccessException("Failed to update post.", ex);
            }
        }

        public void DeletePost(int id)
        {
            try
            {
                _blogPostRepository.DeletePost(id);
            }
            catch (DbException ex)
            {
                throw new DataAccessException("Failed to delete post.", ex);
            }
        }
    }

    public class ValidationException : Exception
    {
        public ValidationException(string message) : base(message) { }
    }
}