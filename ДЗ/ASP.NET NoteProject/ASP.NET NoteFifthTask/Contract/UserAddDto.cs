namespace ConsoleProject.NET.Contract;

public record UserAddDto(string Name, string Password);
public record UserVm(Guid id, string Name);