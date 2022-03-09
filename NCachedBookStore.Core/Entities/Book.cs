using System;
using System.ComponentModel.DataAnnotations;

namespace NCachedBookStore.Contracts.Entities
{
    [Serializable]
    public class Book
    {
        public Book()
        {
            this.AddedOn = DateTime.UtcNow;
        }

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ISBN { get; set; }
        public double Price { get; set; }
        public string AuthorName { get; set; }
        public DateTime AddedOn { get; set; }
    }
}
