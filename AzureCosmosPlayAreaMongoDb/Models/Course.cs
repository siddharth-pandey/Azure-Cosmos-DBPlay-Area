using System;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using Newtonsoft.Json;

namespace AzureCosmosPlayAreaMongoDb.Models
{
    public class Course
    {
        [BsonId(IdGenerator = typeof(CombGuidGenerator))]
        public Guid Id { get; set; }

        [BsonElement("Title")]
        public string Title { get; set; }

        [BsonElement("Modules")]
        public string Modules { get; set; }

        [BsonElement("Completed")]
        public bool Completed { get; set; }
    }
}