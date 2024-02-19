using System.Data.Common;

namespace backendSocial.Model
{
    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly BlogPostDbContext _context;

        public BlogPostRepository(BlogPostDbContext context)
        {
            _context = context;
        }

        public void CreatePost(BlogPost blogPost)
        {
            if (!ValidationUtils.IsValid(blogPost))
            {
                throw new InvalidPostException("Title, Author, and Content are required fields.");
            }

            _context.BlogPost.Add(blogPost);
            try
            {
                _context.SaveChanges();
            }
            catch (DbException ex)
            {
                throw new DatabaseException("Failed to create post.", ex);
            }
        }

        public List<BlogPost> ReadAllPosts()
        {
            try
            {
                return _context.BlogPost.OrderByDescending(p => p.DateCreated).ToList();
            }
            catch (DbException ex)
            {
                throw new DataAccessException("Failed to retrieve all posts.", ex);
            }
        }

        public BlogPost ReadPostById(int id)
        {
            var post = _context.BlogPost.Find(id);
            if (post == null)
            {
                throw new PostNotFoundException($"Post with ID {id} not found.");
            }
            return post;
        }

        public void UpdatePost(BlogPost blogPost)
        {
            var existingPost = _context.BlogPost.Find(blogPost);
            if (existingPost == null)
            {
                throw new PostNotFoundException($"Post with ID {blogPost.Id} not found for update.");
            }

            existingPost.Title = blogPost.Title;
            existingPost.Author = blogPost.Author;
            existingPost.Content = blogPost.Content;

            try
            {
                _context.SaveChanges();
            }
            catch (DbException ex)
            {
                throw new DatabaseException("Failed to update post.", ex);
            }
        }

        public void DeletePost(int id)
        {
            var post = _context.BlogPost.Find(id);
            if (post == null)
            {
                throw new PostNotFoundException($"Post with ID {id} not found for deletion.");
            }

            _context.BlogPost.Remove(post);

            try
            {
                _context.SaveChanges();
            }
            catch (DbException ex)
            {
                throw new DatabaseException("Failed to delete post.", ex);
            }
        }
    }

    public class InvalidPostException : Exception
    {
        public InvalidPostException(string message) : base(message) { }
    }

    public class PostNotFoundException : Exception
    {
        public PostNotFoundException(string message) : base(message) { }
    }

    public class DataAccessException : Exception
    {
        public DataAccessException(string message, Exception innerException) : base(message, innerException) { }
    }

    public class DatabaseException : Exception
    {
        public DatabaseException(string message, Exception innerException) : base(message, innerException) { }
    }
}