using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace ConsoleProject.NET.Politics;

public class PostOwnerRequirementHandler(IHttpContextAccessor accessor)
: AuthorizationHandler<PostOwnerRequirement>
{
    protected override Task HandleRequirementAsync(
    AuthorizationHandlerContext context,
    PostOwnerRequirement requirement)
    {
        // Ищем в контексте у пользователя клейм с идентификатором.
        var userIdClaim = context.User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim is null)
        {
            context.Fail();
            return Task.CompletedTask;
        }
        // Тут мы закладываемся на то, что везде, где используем эту политику
        // id пользователя будет идти с именем userId.
        // Получаем httpContext запроса, чтобы достать из строки запроса userId.
        var accessorResult = accessor
        .HttpContext!
        .Request
        .Query
        .TryGetValue("userId", out var userIdQuery);
        if (!accessorResult || !userIdQuery.Any())
        {
            context.Fail();
            return Task.CompletedTask;
        }
        // Проверяем, что заголовок содержит того же пользователя, что и строка запроса.
        if (userIdClaim.Value != userIdQuery.First())
        {
            context.Fail();
            return Task.CompletedTask;
        }
        context.Succeed(requirement);
        return Task.CompletedTask;
    }
}
