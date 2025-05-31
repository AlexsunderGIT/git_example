using System.Reflection;
using ConsoleProject.NET;
using ConsoleProject.NET.Database;
using ConsoleProject.NET.Repositories;
using ConsoleProject.NET.Services;
using Microsoft.EntityFrameworkCore;

namespace SimpleExample;
public static class Composer
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(Composer).Assembly);
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseNpgsql("Host=localhost;Port=5432;Username=postgres;Password=a2356767Z;Database=NoteProj");
        });
        services.AddExceptionHandler<ExceptionHandler>();
        services.AddControllers();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<INoteRepository, NoteRepository>();
        return services;
    }

    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        return services;
    }
}
