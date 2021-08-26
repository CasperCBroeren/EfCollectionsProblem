using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace EfCollections.Models
{
    public class Person
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Book> BooksRead { get; set; }
    }
}
