using System.Data.Common;

namespace backendSocial.Model
{
    public class PostRepository : IPostRepository
    {
        private readonly PostDbContext _context;

        public PostRepository(PostDbContext context)
        {
            _context = context;
        }

        public void CreatePost(Post post)
        {
            if (!ValidationUtils.IsValid(post))
            {
                throw new InvalidPostException("Title, Author, and Content are required fields.");
            }

            _context.Post.Add(post);
            try
            {
                _context.SaveChanges();
            }
            catch (DbException ex)
            {
                throw new DatabaseException("Failed to create post.", ex);
            }
        }

        public List<Post> ReadAllPosts()
        {
            try
            {
                return _context.Post.OrderByDescending(p => p.DateCreated).ToList();
            }
            catch (DbException ex)
            {
                throw new DataAccessException("Failed to retrieve all posts.", ex);
            }
        }

        public Post ReadPostById(int id)
        {
            var post = _context.Post.Find(id);
            if (post == null)
            {
                throw new PostNotFoundException($"Post with ID {id} not found.");
            }
            return post;
        }

        public void UpdatePost(Post post)
        {
            var existingPost = _context.Post.Find(post.Id);
            if (existingPost == null)
            {
                throw new PostNotFoundException($"Post with ID {post.Id} not found for update.");
            }

            existingPost.Title = post.Title;
            existingPost.Author = post.Author;
            existingPost.Content = post.Content;

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
            var post = _context.Post.Find(id);
            if (post == null)
            {
                throw new PostNotFoundException($"Post with ID {id} not found for deletion.");
            }

            _context.Post.Remove(post);

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