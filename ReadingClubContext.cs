using EfCollections.Models;
using Microsoft.EntityFrameworkCore;

namespace EfCollections
{
    public class ReadingClubContext : DbContext
    {
        public DbSet<Book> Books { get; set; }

        public DbSet<Person> Persons { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=..\\..\\..\\CustomerDB.db;");

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { 
        }
    }
}
