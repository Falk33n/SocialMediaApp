using backendSocial.Model;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // Register database context if using Entity Framework
        // e.g., builder.Services.AddDbContext<MyDbContext>();

        // Register PostService and its dependency
        builder.Services.AddDbContext<PostDbContext>();
        builder.Services.AddTransient<PostService, PostService>();
        builder.Services.AddTransient<IPostRepository, PostRepository>();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}