using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using EfCollections.Models;
 
using Microsoft.EntityFrameworkCore;

namespace EfCollections
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello db test");

            using (var db = new ReadingClubContext())
            {
                db.Database.EnsureCreated();
                Setup(db);
                Console.WriteLine("Books: " + string.Join(", ", db.Books.ToList().Select(b => b.Title)));

                Console.WriteLine("Persons: " + string.Join(Environment.NewLine,
                    db.Persons.Include(p => p.BooksRead).ToList().Select(p => $"{p.Name} ({p.BooksRead.Count})")));
            }

            using (var db = new ReadingClubContext())
            {

                var fakeJohn = new Person()
                {
                    Id = 2,
                    BooksRead = new List<Book>()
                    {
                        new() { Id = 1, Title = "Jungle Book" },
                        new() { Id = 2, Title = "Tom Swayer" }
                    },
                    Name = "John Books1"
                };
                db.Persons.Update(fakeJohn);
                db.SaveChanges();
            }
            using (var db = new ReadingClubContext())
            {
                Console.WriteLine("Persons: " + string.Join(Environment.NewLine,
                    db.Persons.Include(p => p.BooksRead).ToList().Select(p => $"{p.Name} ({p.BooksRead.Count})")));
            }
        }

        private static void Setup(ReadingClubContext db)
        {
            if (db.Books.Count() == 0)
            {
                var jungleBook = new Book() { Id = 1, Title = "Jungle Book" };
                var tomSawyer = new Book() { Id = 2, Title = "Tom Swayer" };
                var mobyDick = new Book() { Id = 3, Title = "Moby Dick" };
                db.Books.Add(jungleBook);
                db.Books.Add(tomSawyer);
                db.Books.Add(mobyDick);
                db.SaveChanges();
                
                var casper = new Person()
                {
                    Id = 1,
                    BooksRead = new List<Book>()
                    {
                        jungleBook,
                        tomSawyer,
                        mobyDick
                    },
                    Name = "Casper"
                };

                var john = new Person()
                {
                    Id = 2,
                    BooksRead = new List<Book>()
                    {
                        jungleBook,
                    },
                    Name = "John"
                };

                db.Persons.Add(john);
                db.Persons.Add(casper);
                db.SaveChanges();
            }
        }
    }
}
