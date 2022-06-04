using System;
using System.Linq;

namespace EFGetStarted;

// Commands to execute to create the SQLLite Db (do not forget the Update-Database command !)
//Add-Migration InitialCreate
//Update-Database

//Remove-Migration

internal class Program
{
    private static void Main()
    {
        using var db = new MessageContext();

        // Note: This sample requires the database to be created before running.
        Console.WriteLine($"Database path: {db.DbPath}.");

        // Create
        Console.WriteLine("Inserting a new blog");
        db.Add(new Message());
        db.SaveChanges();

        // Read
        Console.WriteLine("Querying for a blog");
        var blog = db.Messages
            .OrderBy(b => b.MessageId)
            .First();

        // Update
        Console.WriteLine("Updating the blog and adding a post");
        blog.Body.Add("key", "value");
        db.SaveChanges();

        // Read
        Console.WriteLine("Querying for a blog");
        var blog2 = db.Messages
            .OrderBy(b => b.MessageId)
            .First();

        // Delete
        Console.WriteLine("Delete the blog");
        db.Remove(blog);
        db.SaveChanges();
    }
}