using System;
using Avalonia.NETCoreMVVMApp1.Models;
using Microsoft.EntityFrameworkCore;

namespace Avalonia.NETCoreMVVMApp1.Data;

public class CollectionContext : DbContext
{
    public DbSet<CollectionCategory> Categories { get; set; }
    public DbSet<CollectionEntity> Entities { get; set; }
    public string DbPath { get; }

    public CollectionContext()
    {
        // var folder = System.IO.Directory.GetCurrentDirectory();
        var folder = System.Reflection.Assembly.GetExecutingAssembly().Location;
	var directory = System.IO.Path.GetDirectoryName(folder);
        DbPath = System.IO.Path.Join(directory, "collections.db");
        Console.WriteLine(DbPath);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlite($"Data Source={DbPath};Mode=ReadWrite;");
    }
}