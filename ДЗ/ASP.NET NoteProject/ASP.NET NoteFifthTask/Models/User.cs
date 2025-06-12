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
using ConsoleProject.NET.Database;

namespace ConsoleProject.NET.Models;

public class User
{
    public Guid Id { get; set; }
    public required string Name { get; set; } 
    public required byte[] Password { get; set; } 
    public virtual ICollection<Note> Notes { get; set; } = new List<Note>();
}