using System.Data.Common;

namespace backendSocial.Model
{
    public class PostService
    {
        private readonly IPostRepository _postRepository;

        public PostService(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public void CreatePost(Post post)
        {
            if (!ValidationUtils.IsValid(post))
            {
                throw new ValidationException("Title, Author, and Content are required fields.");
            }

            try
            {
                _postRepository.CreatePost(post);
            }
            catch (DbException ex)
            {
                throw new DataAccessException("Failed to create post.", ex);
            }
        }

        public List<Post> ReadAllPosts()
        {
            try
            {
                return _postRepository.ReadAllPosts();
            }
            catch (DbException ex)
            {
                throw new DataAccessException("Failed to retrieve all posts.", ex);
            }
        }

        public Post ReadPostById(int id)
        {
            try
            {
                var post = _postRepository.ReadPostById(id);
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

        public void UpdatePost(Post post)
        {
            if (!ValidationUtils.IsValid(post))
            {
                throw new ValidationException("Title, Author, and Content are required fields.");
            }

            try
            {
                _postRepository.UpdatePost(post);
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
                _postRepository.DeletePost(id);
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