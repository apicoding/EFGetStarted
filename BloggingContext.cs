using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace EFGetStarted;

public class BloggingContext : DbContext
{
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Stock> Stocks { get; set; }
    public DbSet<Valuation> Valuations { get; set; }

    public string DbPath { get; }

    public BloggingContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = System.IO.Path.Join(path, "blogging.db");
    }

    // The following configures EF to create a Sqlite database file in the
    // special "local" folder for your platform.
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");
}

public class Blog
{
    public int BlogId { get; set; }
    public string Url { get; set; }

    public List<Post> Posts { get; } = new();
}

public class Post
{
    public int PostId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }

    public int BlogId { get; set; }
    public Blog Blog { get; set; }
}

public class Stock
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    [MaxLength(8)]
    public string Symbol { get; set; }

    [OneToMany(CascadeOperations = CascadeOperation.All)]      // One to many relationship with Valuation
    public List<Valuation> Valuations { get; set; }
}

public class Valuation
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    [ForeignKey(typeof(Stock))]     // Specify the foreign key
    public int StockId { get; set; }
    public DateTime Time { get; set; }
    public decimal Price { get; set; }

    [ManyToOne]      // Many to one relationship with Stock
    public Stock Stock { get; set; }
}