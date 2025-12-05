using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace TomadaStore.Models.DTOs.Category
{
    public class CategoryResponseDTO
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; init; }

        [BsonElement("name")]
        public string Name { get; init; }

        [BsonElement("description")]
        public string Description { get; init; }
    }
}
