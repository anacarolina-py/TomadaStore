using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace TomadaStore.Models.Models
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }
        public Category Category { get; private set; }
       public Product()
        {
        }
        public Product(ObjectId id, string name, string description, decimal price, Category category)
        {
            Id = Id;
            Name = name;
            Description = description;
            Price = price;
            Category = category;
        }
    }
}
