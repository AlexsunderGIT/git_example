using System.Reflection;
using System.Security.Claims;
using ConsoleProject.NET;
using ConsoleProject.NET.Abstractions;
using ConsoleProject.NET.Database;
using ConsoleProject.NET.Politics;
using ConsoleProject.NET.Repositories;
using ConsoleProject.NET.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.SwaggerGen;

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

        services.AddOptions<JwtOptions>()
                .Bind(configuration.GetRequiredSection(nameof(JwtOptions)))
                .ValidateDataAnnotations()
                .ValidateOnStart();
        services.AddSwaggerGen();
        services.AddTransient<IJwtTokenGenerator, JwtTokenGenerator>();

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                 .AddJwtBearer(options =>
                 {
                     // Достаем настройки из конфига тут, чтобы не хардкодить.
                     var jwtOptions = configuration
                     .GetRequiredSection(nameof(JwtOptions))
                     // Восклицательный знак, чтобы указать, что тут 100% не null. (выше мы уже сделали ValidateOnStart)
                     .Get<JwtOptions>()!;
                     options.TokenValidationParameters = new TokenValidationParameters
                     {
                         // Проверять ли того, кто выдал токен.
                         ValidateIssuer = true,
                         // Проверять ли того, кому выдан токен.
                         ValidateAudience = true,
                         // Проверять ли время жизни токена.
                         ValidateLifetime = true,
                         // Проверять ли секретный ключ.
                         ValidateIssuerSigningKey = true,
                         // Сервис, который отвечает за выдачу токена.
                         ValidIssuer = jwtOptions.Issuer,
                         // Сервис, которому можно выдавать токен.
                         ValidAudience = jwtOptions.Audience,
                         // Секрет, который используется для шифрования.
                         IssuerSigningKey = new
              SymmetricSecurityKey(Convert.FromBase64String(jwtOptions.Secret)),
                     };
                     options.Events = new JwtBearerEvents
                     {
                         // Валидация токена.
                         OnTokenValidated = context =>
                         {

                             // Так как мы пока еще в регистрации сервисов, мы только так сможем достать IAuthService.
                             var authService =
                                  context.HttpContext
                                     .RequestServices
                                     .GetRequiredService<IAuthService>();
                             // Достаем идентификатор пользователя из клеймов.
                             var userId = context.Principal?.FindFirstValue(ClaimTypes.NameIdentifier);
                             if (
                             // Если не нашли пользователя, не пускаем.
                             userId is null
                             // Если токен истек, не пускаем.
                             || context.SecurityToken.ValidTo < DateTime.UtcNow
                             // Если токен не наш, не пускаем.
                             || !authService.VerifyToken(Guid.Parse(userId),
                             context.SecurityToken.UnsafeToString()))
                             {
                                 context.Fail("Unauthorized");
                             }
                             return Task.CompletedTask;
                         }
                     };
                 });

        services.AddScoped<IAuthorizationHandler, PostOwnerRequirementHandler>();
        services.AddHttpContextAccessor();

        services.AddAuthorization(options =>
        {
            var defaultAuthorizationPolicyBuilder =
            new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme);
            defaultAuthorizationPolicyBuilder.RequireAuthenticatedUser();
            options.DefaultPolicy = defaultAuthorizationPolicyBuilder.Build();
        // Добавляем политику проверки пользователя из токена.
            options.AddPolicy("PostsOwner", policy =>
            {
                // Перед тем как проверять пользователя, отсекаем всех не аутентифицированных.
                policy.RequireAuthenticatedUser();
                policy.AddRequirements(new PostOwnerRequirement());
            });
        });
        return services;

    }
    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
        services.AddEndpointsApiExplorer();
        return services;
    }


}
