using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BookStoreMongoDb.Server.Models.Contracts
{
    public interface IAuditableDocument
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        ObjectId Id { get; set; }
        DateTime CreatedOn { get; set; }
        DateTime? LastModifiedOn { get; set; }
    }
}