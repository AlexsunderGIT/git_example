using System.Reflection;
using ConsoleProject.NET;
using ConsoleProject.NET.Repositories;
using ConsoleProject.NET.Services;

namespace SimpleExample;
     public static class Composer
     {   
            // Тут будем добавлять инфраструктурные сервисы 
            // (которые нужны для передачи данных внутри приложения).
            public static IServiceCollection AddInfrastructure(this IServiceCollection services)
            {

                // Добавляем автомаппер и регистрируем в нем все классы из
                // нашего проекта, которые мы наследовали от Profile
                services.AddAutoMapper(typeof(Composer).Assembly);
                services.AddExceptionHandler<ExceptionHandler>();
                services.AddControllers();
                services.AddSingleton<IUserRepository, UserRepository>();
                services.AddSingleton<INoteRepository, NoteRepository>();
                return services;
    }
    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        return services;
    }
 }
