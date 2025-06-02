using ConsoleProject.NET.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConsoleProject.NET.Database;

public class UserEntityConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(user => user.Id);
        builder.Property(user => user.Name)
            .IsRequired()
            .HasMaxLength(32);
        builder.Property(user => user.Password)
            .IsRequired()
            .HasMaxLength(28);
        builder.HasMany(user => user.Notes)
                .WithOne(note => note.User)
                .HasForeignKey(note => note.UserId)
                .OnDelete(DeleteBehavior.Cascade);

    }
}


