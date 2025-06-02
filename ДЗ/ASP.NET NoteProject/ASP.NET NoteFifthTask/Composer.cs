using System.Reflection;
using ConsoleProject.NET;
using ConsoleProject.NET.Database;
using ConsoleProject.NET.Repositories;
using ConsoleProject.NET.Services;
using Microsoft.EntityFrameworkCore;

namespace SimpleExample;
public static class Composer
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(typeof(Composer).Assembly);
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<INoteRepository, NoteRepository>();

        services
            .AddOptions<ApplicationDbContextSettings>()
            .Bind(configuration.GetRequiredSection(nameof(ApplicationDbContextSettings)))
            .ValidateDataAnnotations()
            .ValidateOnStart();
        services.Configure<ApplicationDbContextSettings>( 
            configuration.GetRequiredSection(nameof(ApplicationDbContextSettings)));

        services.AddDbContext<AppDbContext>();
        services.AddExceptionHandler<ExceptionHandler>();
        services.AddControllers();
        return services;
    }
    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        return services;
    }
}
