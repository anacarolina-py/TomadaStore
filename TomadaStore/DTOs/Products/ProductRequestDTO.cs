using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using TomadaStore.Models.DTOs.Category;
using TomadaStore.Models.Models;

namespace TomadaStore.Models.DTOs.Product
{
    public class ProductRequestDTO
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("description")]
        public string Description { get; set; }

        [BsonElement("price")]
        public decimal Price { get; set; }

        [BsonElement("category")]
        public CategoryRequestDTO Category { get; set; }
    }
}
