using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text.Json;
using System.IO;
using System.Linq;
using System.Collections.Concurrent;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using System.ComponentModel.DataAnnotations;

namespace ConsoleProject.NET.Models
{
    public class Note
    {
        public int Id { get; set; }

        [MaxLength(20)]
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime NoteCreationTime { get; /*private*/ set; }
        public bool IsCompleted { get; set; }
        [Required]
        public Priority Priority { get; set; }
        [Required]
        public int UserId { get; set; }
        public User User { get; set; }
    }
    public enum Priority
    {
        lowest,
        middle,
        higher
    }
    public class NoteRequest
    {
        public string Title { get; set; } = null;
        public string Description { get; set; } = null;
        public Priority Priority { get; set; }
        public int UserId { get; set; }
    }
    public class NoteUpdateRequest
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public bool? IsCompleted { get; set; }
        public Priority? Priority { get; set; }
    }
    public class FiltredSortedNote
    {
        public Priority? Priority { get; set; }
        public bool? IsCompleted { get; set; }
        public string? DataSort { get; set; }
        public bool SortDescending { get; set; } = false;
    }
}
