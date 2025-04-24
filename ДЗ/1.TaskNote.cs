using System.Collections.Generic;
using System;

namespace Cslight.ДЗ
{
    class Owner
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime OwnerRegistrationTime { get; set; } = DateTime.Now;
        public List<Note> Notes { get; set; } = new List<Note>();
        public Owner(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }

    class Note
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime NoteCreationTime { get; set; } = DateTime.Now;
        public int OwnerId { get; set; }
        public Note(string title, string description, int ownerId)
        {
            Title = title;
            Description = description;
            OwnerId = ownerId;
        }
    }
}