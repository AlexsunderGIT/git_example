using System;
using ConsoleProject.NET.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
namespace ConsoleProject.NET.Database;

public class AppDbContext : DbContext
{
    private readonly IWebHostEnvironment _environment;
    private readonly ILogger<AppDbContext> _logger;
    private readonly ApplicationDbContextSettings _dbContextSettings;
    public DbSet<User> Users => Set<User>();
    public DbSet<Note> Notes => Set<Note>();
    public virtual DbSet<JwtToken> JwtTokens { get; set; }
    public AppDbContext(
        DbContextOptions<AppDbContext> options, 
        IOptions<ApplicationDbContextSettings> dbContextSettings,
        IWebHostEnvironment environment,
        ILogger<AppDbContext> logger
        ) : base(options)
    {
        _dbContextSettings = dbContextSettings.Value;
        _environment = environment;
        _logger = logger;
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseNpgsql(_dbContextSettings.ConnectionString);

            optionsBuilder.EnableSensitiveDataLogging();
            if (_environment.IsDevelopment())
            {
                optionsBuilder.EnableSensitiveDataLogging();
                optionsBuilder.LogTo(message => _logger.LogInformation(message),
                LogLevel.Information);
            }
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(Program).Assembly);
    }
}