using backendSocial.Model;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddDbContext<BlogPostDbContext>();
        builder.Services.AddTransient<BlogPostService, BlogPostService>();
        builder.Services.AddTransient<IBlogPostRepository, BlogPostRepository>();

        var app = builder.Build();

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}