namespace ConsoleProject.NET.Contract;

public record JwtTokenVm(Guid UserId, string Token, DateTime ExpiresAt);
