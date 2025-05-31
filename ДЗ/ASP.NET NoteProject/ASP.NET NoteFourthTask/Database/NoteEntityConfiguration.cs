using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ConsoleProject.NET.Models;

namespace ConsoleProject.NET.Database;

public class NoteEntityConfiguration : IEntityTypeConfiguration<Note>
{
    public void Configure(EntityTypeBuilder<Note> builder)
    {
        builder.HasKey(note => note.Id);

        builder.Property(note => note.Title)
            .IsRequired()
            .HasMaxLength(300);

        builder.Property(note => note.Description)
            .IsRequired()
            .HasColumnType("text");

        builder.HasOne(note => note.User)
        .WithMany(user => user.Notes)
        .HasForeignKey(note => note.UserId);
    }
}


