namespace ConsoleProject.NET.Contract;

public record UserAddDto(string Name, string Password);
public record UserVm(int id, string Name);