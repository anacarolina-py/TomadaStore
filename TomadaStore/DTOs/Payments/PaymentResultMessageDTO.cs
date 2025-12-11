using System;
using System.Collections.Generic;
using System.Text;
using TomadaStore.Models.DTOs.Customer;
using TomadaStore.Models.DTOs.Product;

namespace TomadaStore.Models.DTOs.Payments
{
    public class PaymentResultMessageDTO
    {
        public CustomerRequestDTO Customer { get; set; }
        public List<ProductRequestDTO> Products {get; set;}
        public decimal TotalPrice { get; set; }
        public string Status { get; set; }
    }
}
