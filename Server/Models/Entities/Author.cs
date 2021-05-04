using System;
using BookStoreMongoDb.Server.Models.Contracts;

namespace BookStoreMongoDb.Server.Models.Entities
{
    [BsonCollection("authors")]
    public class Author : AuditableDocument
    {
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public string ShortBio { get; set; }
    }
}
