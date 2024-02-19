namespace backendSocial.Model
{
    public interface IBlogPostRepository
    {
        void CreatePost(BlogPost post);
        List<BlogPost> ReadAllPosts();
        BlogPost ReadPostById(int id);
        void UpdatePost(BlogPost post);
        void DeletePost(int id);
    }
}