namespace backendSocial.Model
{
    public static class ValidationUtils
    {
        public static bool IsValid(Post post)
        {
            return !string.IsNullOrEmpty(post.Title) && !string.IsNullOrEmpty(post.Author) && !string.IsNullOrEmpty(post.Content);
        }
    }
}