using System;
using BookStoreMongoDb.Server.Models.Contracts;

namespace BookStoreMongoDb.Server.Models.Entities
{
    [BsonCollection("books")]
    public class Book : AuditableDocument
    {
        public string AuthorId { get; set; }
        public string Name { get; set; }
        public BookType Type { get; set; }

        public DateTime PublishDate { get; set; }

        public float Price { get; set; }
    }

    public enum BookType
    {
        Undefined = 0,
        Adventure = 1,
        Biography = 2,
        Dystopia = 3
    }
}
