namespace backendSocial.Model
{
    public static class ValidationUtils
    {
        public static bool IsValid(BlogPost blogPost)
        {
            return !string.IsNullOrEmpty(blogPost.Title) && !string.IsNullOrEmpty(blogPost.Author) && !string.IsNullOrEmpty(blogPost.Content);
        }
    }
}