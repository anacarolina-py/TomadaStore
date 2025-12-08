using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace TomadaStore.Models.Models
{
    public class Sale
    {
        public ObjectId Id { get; private set; }
        public Customer Customer { get; private set; }
        public List<Product> Products { get; private set; }
        public DateTime SaleDate { get; private set; }
        public decimal TotalPrice { get; private set; }

        public Sale(Customer customer, List<Product> products, decimal totalPrice)
        {
            Id = ObjectId.GenerateNewId();
            Customer = customer;
            Products = products;
            SaleDate = DateTime.UtcNow;
            TotalPrice = totalPrice;
        }
    }
}
