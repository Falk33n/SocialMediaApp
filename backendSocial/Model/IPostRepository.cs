namespace backendSocial.Model
{
    public interface IPostRepository
    {
        void CreatePost(Post post);
        List<Post> ReadAllPosts();
        Post ReadPostById(int id);
        void UpdatePost(Post post);
        void DeletePost(int id);
    }
}