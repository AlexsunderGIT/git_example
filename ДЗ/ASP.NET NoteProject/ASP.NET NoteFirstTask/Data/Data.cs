using ConsoleProject.NET.Models;
using ConsoleProject.NET.Controllers;
namespace ConsoleProject.NET.Data
{
    public static class DataStorage
    {
        public static readonly List<User> Users = new();
        public static readonly List<Note> Notes = new();
    }
}
