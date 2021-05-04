using MongoDB.Bson;
using System;

namespace BookStoreMongoDb.Server.Models.Contracts
{
    public abstract class AuditableDocument : IAuditableDocument
    {
        public ObjectId Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? LastModifiedOn { get; set; }
    }
}