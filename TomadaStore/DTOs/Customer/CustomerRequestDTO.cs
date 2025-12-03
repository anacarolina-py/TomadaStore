using System;
using System.Collections.Generic;
using System.Text;

namespace TomadaStore.Models.DTOs.Customer
{
    public class CustomerRequestDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
