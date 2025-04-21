using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cslight.ДЗ
{
    internal class Class1
    {

    }
    class Owner
    {

        string nameOwner;
        DateTime OwnerRegistration = DateTime.Now;
        List<Note> ownerNotes = new List<Note>();


    };

    class Note
    {
        string nameNote;
        string description;
        bool isCompleted;
        DateTime NoteCreation = DateTime.Now;
        string owner = nameof(Owner);
    }
}
