using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using SQLite;
using SQLiteNetExtensions.Extensions;

namespace EFGetStarted;

internal class Program
{
    private static void Main()
    {
        using var dbContext = new BloggingContext();

   
        // Note: This sample requires the database to be created before running.
        Console.WriteLine($"Database path: {dbContext.DbPath}.");


        //// Create
        //Console.WriteLine("Inserting a new stock");
        //dbContext.Add(new Stock { Id = 1, Symbol = "TEST", Valuations = new List<Valuation>()});
        //dbContext.SaveChanges();



        // Create
        Console.WriteLine("Inserting a new blog");
        dbContext.Add(new Blog { Url = "http://blogs.msdn.com/adonet" });
        dbContext.SaveChanges();

        // Read
        Console.WriteLine("Querying for a blog");
        var blog = dbContext.Blogs
            .OrderBy(b => b.BlogId)
            .First();

        // Update
        Console.WriteLine("Updating the blog and adding a post");
        blog.Url = "https://devblogs.microsoft.com/dotnet";
        blog.Posts.Add(
            new Post { Title = "Hello World", Content = "I wrote an app using EF Core!" });
        dbContext.SaveChanges();

        // Delete
        Console.WriteLine("Delete the blog");
        dbContext.Remove(blog);
        dbContext.SaveChanges();

        /*
        var db = new SQLiteConnection(context.DbPath);
        db.CreateTable<Stock>();
        db.CreateTable<Valuation>();

        var dollar = new Stock()
        {
            Symbol = "$"
        };
        db.Insert(dollar);   // Insert the object in the database

        var valuation = new Valuation()
        {
            Price = 15,
            Time = DateTime.Now,
        };
        db.Insert(valuation);   // Insert the object in the database

        // Objects created, let's stablish the relationship
        dollar.Valuations = new List<Valuation> { valuation };

        db.UpdateWithChildren(dollar);   // Update the changes into the database
        if (valuation.Stock == dollar)
        {
            Debug.WriteLine("Inverse relationship already set, yay!");
        }

        // Get the object and the relationships
        var storedValuation = db.GetWithChildren<Valuation>(valuation.Id);
        if (dollar.Symbol.Equals(storedValuation.Stock.Symbol))
        {
            Debug.WriteLine("Object and relationships loaded correctly!");
        }
        */
    }
}